using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class DistrictRepo : IDistrictRepo
    {
        private readonly ApplicationContext _applicationContext;

        public DistrictRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        

        public async Task<IEnumerable<District>> GetDistrictsByCityIdAsync(Guid cityId)
        {
            return await _applicationContext.Districts.Where(d => d.CityId == cityId).ToListAsync();
        }

        public async Task<District> GetDistrictsIdAsync(Guid Id)
        {
            return await _applicationContext.Districts.FindAsync(Id);
        }
    }
}
