using StockLens.Dtos.IndustriesDtos;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Industries;
using IndustriesModel = StockLens.Models.Industries;

namespace StockLens.Services.Industries
{
    public class IndustriesService : IIndustriesService
    {
        private readonly IIndustriesRepository _industriesRepo;

        public IndustriesService(IIndustriesRepository industriesRepo)
        {
            _industriesRepo = industriesRepo;
        }

        public async Task BulkCreateIndustriesAsync(List<CreateIndustryDto> dtos)
        {
            IEnumerable<IndustriesModel> inds = dtos.Select(i => i.ToIndustriesFromDto());
            await _industriesRepo.BulkCreateIndustriesAsync(inds.ToList());
        }
        public async Task AddIndustriesAsync(CreateIndustryDto dto)
        {
            IndustriesModel industry = dto.ToIndustriesFromDto();
            await _industriesRepo.AddIndustriesAsync(industry);
        }

        public async Task<IReadOnlyList<GetIndustryDto>> GetIndustriesFilteredAsync(IndustriesQuery query)
        {
            //TODO: Сделать после тикеров
            //if (query.InsideTickers)
            //{
                //IReadOnlyList<IndustriesModel> industriesTickers = await _industriesRepo.GetIndustriesFilteredAsync(query.SectorId);
                //return industries.Select(i => i.ToDtoFromIndustries()).ToList();
            //}

            IReadOnlyList<IndustriesModel> industries = await _industriesRepo.GetIndustriesFilteredAsync(query.SectorId);
            return industries.Select(i => i.ToDtoFromIndustries()).ToList();

        }

        public async Task<GetIndustryDto?> GetIndustryAsync(int industryId)
        {
            var ind = await _industriesRepo.GetIndustryAsync(industryId);
            if (ind != null)
                return ind.ToDtoFromIndustries();
            return null;
        }
    }
}
