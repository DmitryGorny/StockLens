using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.QuotesDtos;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Industries;
using StockLens.Services.HttpRequester;
using IndustriesModel = StockLens.Models.Industries;

namespace StockLens.Services.Industries
{
    public class IndustriesService : IIndustriesService
    {
        private readonly IIndustriesRepository _industriesRepo;
        private readonly IHttpRequester _analyticsRequester;

        public IndustriesService(IIndustriesRepository industriesRepo, IHttpRequester httpRequester)
        {
            _industriesRepo = industriesRepo;
            _analyticsRequester = httpRequester;
        }

        public async Task<IReadOnlyList<GetIndustryDto>> BulkCreateIndustriesAsync(List<CreateIndustryDto> dtos)
        {
            List<IndustriesModel> inds = dtos.Select(i => i.ToIndustriesFromDto()).ToList();
            await _industriesRepo.BulkCreateIndustriesAsync(inds);
            foreach (var industry in inds)
            {
                Console.WriteLine(industry.Id);
            }
            return inds.Select(i => i.ToDtoFromIndustries()).ToList();
        }
        public async Task<GetIndustryDto> AddIndustriesAsync(CreateIndustryDto dto)
        {
            IndustriesModel industry = dto.ToIndustriesFromDto();
            await _industriesRepo.CreateIndustriesAsync(industry);
            return industry.ToDtoFromIndustries();
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
