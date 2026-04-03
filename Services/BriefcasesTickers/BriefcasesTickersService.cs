using Microsoft.AspNetCore.Identity;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Repositories.Briefcases;
using StockLens.Repositories.BriefcasesTickers;
using StockLens.Repositories.Tickers;
using StockLens.Services.Tickers;

namespace StockLens.Services.BriefcasesTickers
{
    public class BriefcasesTickersService : IBriefcasesTickersService
    {
        private readonly IBriefcasesRepository _briefcasesRepository;
        private readonly IBriefcasesTickersRepository _briefcasesTickersRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly UserManager<User> _userManager;
        public BriefcasesTickersService(IBriefcasesRepository briefcasesRepository,
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
        public async Task<GetBrifcasesListDto> GetBriefcase(int briefcaseId)
        {
            var dto = await _briefcasesRepository.GetBriefcaseAsync(briefcaseId);
            if (dto == null)
                throw new Exception("Портфель не был найден");
            return dto.ToBriefcaseListDto();
        }
        public async Task CreateBriefcase(CreateBriefcaseDto dto)
        {
            List<CreateBriefcasesTickersDto> dtos = [];

            var briefcase = dto.ToBriefcase();
            var createTask = _briefcasesRepository.CreateBriefcase(briefcase);
            foreach (var pair in dto.tickersIdsAndPercantages)
            {
                var ticker = await _tickersRepository.GetTicker(pair.Key);
                await createTask;
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
    }
}
