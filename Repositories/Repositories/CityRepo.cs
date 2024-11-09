using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class CityRepo : ICityRepo
    {
        private readonly ApplicationContext _applicationContext;

        public CityRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _applicationContext.Cities.Include(c => c.Districts).ToListAsync();
        }

        public async Task<IEnumerable<District>> GetDistrictsByCityIdAsync(Guid cityId)
        {
            return await _applicationContext.Districts.Where(d => d.CityId == cityId).ToListAsync();
        }
    }
}
