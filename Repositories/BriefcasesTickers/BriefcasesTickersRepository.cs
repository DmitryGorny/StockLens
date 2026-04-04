using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
using StockLens.Models;
using System.Collections.Generic;

namespace StockLens.Repositories.BriefcasesTickers
{
    public class BriefcasesTickersRepository : IBriefcasesTickersRepository
    {
        private readonly AppDBContext _dbContext;
        public BriefcasesTickersRepository(AppDBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public async Task CreateBrifcasesTickers(Models.BriefcasesTickers bct)
        {
            _dbContext.BriefcasesTickers.Add(bct);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBriefcasesTickers(int tickerId, int briefcaseId)
        {
            await _dbContext.BriefcasesTickers
                .Where(bct => bct.TickerId == tickerId && bct.BriefcaseId == briefcaseId)
                .FirstOrDefaultAsync();
        }
        public async Task CreateBriefcaseBulk(IEnumerable<CreateBriefcasesTickersDto> dto)
        {
            var models = dto.Select(d => d.ToBriefcaseTickers());
            await _dbContext.BulkInsertAsync(models);
            await _dbContext.BulkSaveChangesAsync();
        }

        public async Task<List<KeyValuePair<int, decimal>>> PatchBriefcasesTickers(int briefcaseId, PatchBriefcasesTickersDto dto)
        {
            List<KeyValuePair<int, decimal>> newTickers = [];

            var bct = await _dbContext.BriefcasesTickers
                                          .Where(bct => bct.BriefcaseId == briefcaseId)
                                          .ToListAsync();

            if (bct.Count() == 0)
                throw new Exception("Такого портфеля нет");

            newTickers = dto.newTickersAndPercantages.Where(p => !bct.Any(b => b.TickerId == p.Key)).ToList();

            foreach (var pair in dto.newTickersAndPercantages)
            {

                var BriefcaseTicker = bct.Find(b => b.TickerId == pair.Key);
                if (BriefcaseTicker == null)
                    continue;

                BriefcaseTicker.percantage = pair.Value;
            }
            await _dbContext.SaveChangesAsync();
            return newTickers;
        }
    }
}
