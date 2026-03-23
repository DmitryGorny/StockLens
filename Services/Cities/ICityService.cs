using Microsoft.Identity.Client;
using StockLens.Dtos.CitiesDtos;

namespace StockLens.Services.Cities
{
    public interface ICityService
    {
        public Task<GetCitiesDto> GetCity(int cityId);
        public Task<IEnumerable<GetCitiesDto>> GetAllCities();
        public Task CreateCity(CreateCitiesDtos dto);
        public Task DeleteCity(int cityId);
    }
}
