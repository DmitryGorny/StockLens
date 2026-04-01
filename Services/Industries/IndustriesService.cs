using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.QuotesDtos;
using StockLens.Mappers;
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

            var ids = dtos.Select(d => d.SectorId);
            var industries = await _industriesRepo.GetIndustriesBySectorsAsync(ids.ToList(), 0, 1000);
        
            return industries.Select(i => i.ToDtoFromIndustries()).ToList(); 
        }
        public async Task<GetIndustryDto> AddIndustriesAsync(CreateIndustryDto dto)
        {
            IndustriesModel industry = dto.ToIndustriesFromDto();
            await _industriesRepo.CreateIndustriesAsync(industry);
            return industry.ToDtoFromIndustries();
        }


        public async Task<GetIndustryDto> GetIndustryAsync(int industryId)
        {
            var ind = await _industriesRepo.GetIndustryAsync(industryId);
            if (ind != null)
                throw new Exception($"Индустрии с id {industryId} не было найдено");
            return ind!.ToDtoFromIndustries();

        }

        public async Task<IEnumerable<GetIndustryDto>> GetIndustriesBySectorAsync(List<int> sectorsIds, int start, int size)
        {
            var inds = await _industriesRepo.GetIndustriesBySectorsAsync(sectorsIds, start, size);
            if (inds == null)
                throw new Exception($"Индустрий для этих секторов {string.Join(", ", sectorsIds)}");
            return inds.Select(i => i.ToDtoFromIndustries());

        }

        public async Task<IEnumerable<GetIndustryDto>> GetAllIndustriesAsync(int start, int size)
        {
            var inds = await _industriesRepo.GetAllIndustriesAsync(start, size);
            if (inds == null)
                throw new Exception($"Индустрии не были найдены");
            return inds.Select(i => i.ToDtoFromIndustries());

        }

        public async Task CreateIndustry(CreateIndustryDto dto)
        {
            IndustriesModel industry = dto.ToIndustriesFromDto();
            if (industry == null)
                throw new Exception($"Индустрия не была создана");
            await _industriesRepo.CreateIndustriesAsync(industry);
        }

        public async Task PatchIndustry(int IndustryId, PatchIndustryDto dto)
        {
            await _industriesRepo.PatchIndustry(IndustryId, dto);
        }

        public async Task DeleteIndustry(int IndustryId)
        {
            var ind = await _industriesRepo.GetIndustryAsync(IndustryId);
            if (ind == null)
                throw new Exception($"Индустрия с id {IndustryId} не была найдена");
            await _industriesRepo.DeleteIndustryHardAsync(ind);
        }
    }
}

