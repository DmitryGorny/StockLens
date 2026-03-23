using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.CitiesDtos;

using CitiesModel = StockLens.Models.Cities;

namespace StockLens.Repositories.Cities
{
    public class CitiesRepository : ICitiesRepositroy
    {
        private readonly AppDBContext _db_context;

        public CitiesRepository(AppDBContext db_context)    
        {
            _db_context = db_context;
        }

        public async Task CreateCitiesBulkAsync(List<CitiesModel> Cities)
        {
            await _db_context.BulkInsertAsync(Cities);
        }
        public async Task<CitiesModel> AddCityAsync(CitiesModel city)
        {

            var existingCity = await _db_context.Cities
                    .FirstOrDefaultAsync(c => c.Name == city.Name);

            if (existingCity != null)
            {
                // Если найден — возвращаем существующий объект
                return existingCity;
            }
            await _db_context.AddAsync(city);
            await _db_context.SaveChangesAsync();
            return city;
        }

        public async Task<CitiesModel?> GetCity(int cityId)
        {
            return await _db_context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<IEnumerable<CitiesModel>> GetAllCitiesAsync()
        {
            return await _db_context.Cities.ToListAsync();
        }
    }
}
