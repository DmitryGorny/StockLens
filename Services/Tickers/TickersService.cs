using Microsoft.AspNetCore.Mvc.Formatters;
using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Tickers;
using System.Collections.Generic;
using TickersModel = StockLens.Models.Tickers;

namespace StockLens.Services.Tickers
{
    public class TickersService : ITickersService
    {
        private readonly ITickersRepository _tickersRepository;

        public TickersService(ITickersRepository tickersRepository)
        {
            _tickersRepository = tickersRepository;
        }

        public async Task<List<GetTickersDto>> BulkCreateTickersAsync(List<CreateTickersDto> dtos)
        {
            List<TickersModel> tickers = dtos.Select(d => d.CreateTickerFromDto()).ToList();
            await _tickersRepository.BulkCreateTickersAsync(tickers);
            return tickers.Select(t => t.CreateDtoFromTickers()).ToList();
        }
        public async Task AddTikersAsync(CreateTickersDto dto)
        {
            await _tickersRepository.AddTickerAsync(dto.CreateTickerFromDto());
        }
        public async Task<GetTickersDto?> GetTickerAsync(int industryId)
        {
            var ticker = await _tickersRepository.GetTicker(industryId);
            return ticker.CreateDtoFromTickers();
        }
        public async Task<List<GetTickersDto>?> GetTickersAsync(TickersQuery query)
        {
            IReadOnlyList<TickersModel> tickers;
            if (query.InudustriesId.Count() > 0)
            {
                tickers = await GetTickersByIndustryAsync(query.InudustriesId, query.pageNumber, query.pageSize);

                if (tickers == null) 
                    return null;
            } else
            {

                tickers = await GetAllTickersPaginatedAsync(query.pageNumber, query.pageSize);

                if (tickers == null)
                    return null;
            }

            var filteredList = GetTicketsFiltered(tickers, query);
            if (filteredList != null)
                tickers = filteredList;

            var sortedList = GetTickersSortedAsync(tickers, query);
            if (sortedList != null)
                tickers = sortedList;

            return tickers.Select(t => t.CreateDtoFromTickers()).ToList();


        }

        private async Task<IReadOnlyList<TickersModel>>? GetTickersByIndustryAsync(IReadOnlyCollection<int> ids, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            return await _tickersRepository.GetTickers(ids, skip, pageSize);
        } 

        private async Task<IReadOnlyList<TickersModel>?> GetAllTickersPaginatedAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            return await _tickersRepository.GetTickers(skip, pageSize);
        }

        private IReadOnlyList<TickersModel>? GetTicketsFiltered(IReadOnlyList<TickersModel> tics, TickersQuery query)
        {
            IReadOnlyList<TickersModel>? filtered = null;

            if (query.CityFiltersId != null)
            {
                filtered = tics.Where(t => t.CityId == query.CityFiltersId).ToList();
            }

            return filtered;
        }
        private IReadOnlyList<TickersModel>? GetTickersSortedAsync(IReadOnlyList<TickersModel> tics, TickersQuery query)
        {
            IReadOnlyList<TickersModel>? sorted = null;

            if (query.levelSortDesc != null)
            {
                sorted = (query.levelSortDesc.Value 
                    ? tics.OrderByDescending(t => t.ListLevel) 
                    : tics.OrderBy(t => t.ListLevel)).ToList();
                return sorted;
            }

            if (query.PrivalagedSort != null)
            {
                sorted = (query.PrivalagedSort.Value
                    ? tics.OrderByDescending(t => t.ListLevel)
                    : tics.OrderBy(t => t.ListLevel)).ToList();
                return sorted;
            }

            if (query.PrivalagedSort != null)
            {
                sorted = (query.PrivalagedSort.Value
                    ? tics.OrderByDescending(t => t.ListLevel)
                    : tics.OrderBy(t => t.ListLevel)).ToList();
                return sorted;
            }

            if (query.DividendsSortDesc != null)
            {
                sorted = (query.DividendsSortDesc.Value
                    ? tics.OrderByDescending(t => t.ListLevel)
                    : tics.OrderBy(t => t.ListLevel)).ToList();
                return sorted;
            }

            return sorted;
        }   
    }
}
