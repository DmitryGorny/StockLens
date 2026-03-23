using StockLens.Dtos.SectorDtos;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Repositories.Sector;
using StockLens.Services.HttpRequester;
using SectorModel = StockLens.Models.Sectors;


namespace StockLens.Services.Sector
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task CreateSectorsBulkAsync(List<CreateSectorDto> dtos)
        {
            List<Sectors> sectors = new List<Sectors>();
            foreach (var dto in dtos)
            {
                Sectors sector = dto.CreateSectorFromDto();
                sectors.Add(sector);
            }

            await _sectorRepository.BulkCreateSectorsAsync(sectors);
        }
        public async Task<GetSectorDto> CreateSectorAsync(CreateSectorDto dto)
        {
            Sectors sector = await _sectorRepository.CreateSectorAsync(dto.CreateSectorFromDto());
            return sector.CreateDtoFromSectors();
        }

        public async Task<GetSectorDto> GetSectorAsync(int sectorId)
        {
            var sector = await _sectorRepository.GetSectorAsync(sectorId);
            if (sector == null)
            {
                throw new Exception($"Сектор с {sectorId} не найден");
            }
            return sector.CreateDtoFromSectors();
        }

        public async Task<IEnumerable<GetSectorDto>> GetAllSectorsAsync(int start, int size)
        {
            return (await _sectorRepository.GetAllSectorsAsync(start, size)).Select(s => s.CreateDtoFromSectors());
        }

        public async Task PatchSector(int sectorId, PatchSectorDto dto)
        {
            await _sectorRepository.PatchSectorAsync(sectorId, dto);
        }

        public async Task DeleteSectorHard(int sectorId)
        {
            var sector = await _sectorRepository.GetSectorAsync(sectorId);
            if (sector == null)
            {
                throw new Exception($"Сектор с id {sectorId} не найден");
            }
            await _sectorRepository.DeleteSectorHardAsync(sectorId);
        }
    }
}
