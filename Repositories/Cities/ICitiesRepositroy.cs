using StockLens.Dtos.CitiesDtos;
using StockLens.Models;
using CitiesModel = StockLens.Models.Cities;


namespace StockLens.Repositories.Cities
{
    public interface ICitiesRepositroy
    {
        public Task CreateCitiesBulkAsync(List<CitiesModel> Cities);
        public Task<CitiesModel> AddCityAsync(CitiesModel City);
    }
}
