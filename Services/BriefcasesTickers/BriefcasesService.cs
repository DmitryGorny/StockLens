using Microsoft.AspNetCore.Identity;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Repositories.Briefcases;
using StockLens.Repositories.BriefcasesTickers;
using StockLens.Repositories.Tickers;
using StockLens.Services.Tickers;
using System.Collections.Generic;

namespace StockLens.Services.BriefcasesTickers
{
    public class BriefcasesService : IBriefcasesService
    {
        private readonly IBriefcasesRepository _briefcasesRepository;
        private readonly IBriefcasesTickersRepository _briefcasesTickersRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly UserManager<User> _userManager;
        public BriefcasesService(IBriefcasesRepository briefcasesRepository,
                                        IBriefcasesTickersRepository bct, 
                                        ITickersRepository tics,
                                        UserManager<User> userManager)
        {
            _briefcasesRepository = briefcasesRepository;
            _briefcasesTickersRepository = bct;
            _tickersRepository = tics;
            _userManager = userManager;
        }
        public async Task<IEnumerable<GetBrifcasesListDto>> GetBrifcasesListAsync(string userEmail, int start, int size)
        {
            User? user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                throw new UnauthorizedAccessException("Пользователь не найден");


            var list = await _briefcasesRepository.GetUsersBriefcasesAsync(user.Id, start, size);
            return list.Select(b => b.ToBriefcaseListDto());
              
            
        }
        public async Task<GetBriefcasesDto> GetBriefcase(int briefcaseId)
        {
            var briefcase = await _briefcasesRepository.GetBriefcaseAsync(briefcaseId);
            if (briefcase == null)
                throw new Exception("Портфель не был найден");
            return briefcase.ToBriefcasesDto();
        }
        public async Task CreateBriefcase(string userEmail, CreateBriefcaseDto dto)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            List<CreateBriefcasesTickersDto> dtos = [];

            var briefcase = dto.ToBriefcase(user.Id);
            await _briefcasesRepository.CreateBriefcase(briefcase);
            foreach (var pair in dto.tickersIdsAndPercantages)
            {
                var ticker = await _tickersRepository.GetTicker(pair.Key);
                
                if (ticker == null)              
                    throw new Exception("Тикера с таким id не существует");

                var createDto = new CreateBriefcasesTickersDto
                {
                    Ticker = ticker,
                    TickerId = ticker.Id,
                    Briefcase = briefcase,
                    BriefcaseId = briefcase.BriefcasesId,
                    percantage = pair.Value,
                    
                };
                dtos.Add(createDto);
            }

            await _briefcasesTickersRepository.CreateBriefcaseBulk(dtos);
        }
        public async Task DeleteBriefcase(int briefcaseId)
        {
            var briefcase = await _briefcasesRepository.GetBriefcaseAsync(briefcaseId);
            if (briefcase == null)
                throw new Exception("Портфеля не существует");

            await _briefcasesRepository.DeleteBriefcase(briefcase);
        }

        public async Task PatchBriefcasesTickers(int briefcaseId, PatchBriefcaseDto patchDto)
        {
            var briefcase = await _briefcasesRepository.GetBriefcaseAsync(briefcaseId);
            if (briefcase == null) throw new Exception("Портфеля с таким id не существует");
            await _briefcasesRepository.PatchBriefcase(briefcase, patchDto);

            if (patchDto.Tickers != null)
            {
                var sum = patchDto.Tickers.newTickersAndPercantages.Sum(p => p.Value);
                if (sum != 1)
                    throw new Exception("Суммапроцентов должна быть равна 1");

                var idsAndPercantage = await _briefcasesTickersRepository.PatchBriefcasesTickers(briefcaseId, patchDto.Tickers);

                if (idsAndPercantage.Count() > 0)
                {
                    foreach (var pair in idsAndPercantage)
                    {
                        var ticker = await _tickersRepository.GetTicker(pair.Key);

                        if (ticker == null)
                            throw new Exception("Тикера с таким id не существует");

                        var createDto = new CreateBriefcasesTickersDto
                        {
                            Ticker = ticker,
                            TickerId = ticker.Id,
                            Briefcase = briefcase,
                            BriefcaseId = briefcase.BriefcasesId,
                            percantage = pair.Value,

                        };

                        await _briefcasesTickersRepository.CreateBrifcasesTickers(createDto.ToBriefcaseTickers());
                    }  
                }
            }
               
        }
    }
}
