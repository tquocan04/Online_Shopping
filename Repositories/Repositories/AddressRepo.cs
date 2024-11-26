using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using System.IO;

namespace Repositories.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly ApplicationContext _applicationContext;

        public AddressRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task CreateNewAddress(Address address)
        {
            await _applicationContext.Addresses.AddAsync(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAddress(Address address)
        {
            _applicationContext.Addresses.Remove(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Address?> GetAddressByMultiPKAsync(Guid objectId, Guid districtId, string Street)
        {
            return await _applicationContext.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(ca => ca.ObjectId == objectId
                                            && ca.DistrictId == districtId
                                            && ca.Street == Street);
        }

        public async Task<Address?> GetAddressByObjectIdAsync(Guid objectId)
        {
            return await _applicationContext.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(ca => ca.ObjectId == objectId
                                        && ca.IsDefault);
        }

        public async Task UpdateAddress(Address address)
        {
            _applicationContext.Addresses.Update(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId)
        {
            var result = await _applicationContext.Districts
                                        .Where(d => d.Id == districtId)
                                        .Select(d => d.CityId)
                                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetRegionIdByCityIdAsync(Guid cityId)
        {
            var result = await _applicationContext.Cities
                                        .Where(d => d.Id == cityId)
                                        .Select(d => d.RegionId)
                                        .FirstOrDefaultAsync();
            return result;
        }
    }
}
