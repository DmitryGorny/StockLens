using StockLens.Dtos.CitiesDtos;
using StockLens.Mappers;
using StockLens.Repositories.Cities;

namespace StockLens.Services.Cities
{
    public class CitiesService : ICityService
    {
        private readonly ICitiesRepositroy _citiesRepositroy;
        public CitiesService(ICitiesRepositroy citiesRepositroy) { 
            _citiesRepositroy = citiesRepositroy;
        }

        public async Task<GetCitiesDto> GetCity(int cityId)
        {
            var city = await _citiesRepositroy.GetCity(cityId);
            if (city == null)
                throw new Exception($"Город с {cityId} не был найден");
            return city.ToDtoFromCities();
        }
        public async Task<IEnumerable<GetCitiesDto>> GetAllCities()
        {
            var cities = await _citiesRepositroy.GetAllCitiesAsync();
            if (cities == null || cities.Count() == 0) throw new Exception("Города не были найдены");
            return cities.Select(c => c.ToDtoFromCities());
        }

        public async Task CreateCity(CreateCitiesDtos dto)
        {
            var city = dto.ToCitiesFromDto();
            if (city == null) throw new Exception("Город не был создан");
            await _citiesRepositroy.AddCityAsync(city);
        }

        public async Task DeleteCity(int cityId)
        {
            var city = await _citiesRepositroy.GetCity(cityId);
            if (city == null) throw new Exception($"Город с {cityId} не был найден");
            await _citiesRepositroy.DeleteCityAsync(city);
        }
    }
}
