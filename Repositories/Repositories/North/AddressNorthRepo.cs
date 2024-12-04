using Entities.Context;
using Entities.Entities.North;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces.North;

namespace Repositories.Repositories.North
{
    public class AddressNorthRepo : IAddressNorthRepo
    {
        private readonly ApplicationContextNorth _applicationContextNorth;

        public AddressNorthRepo(ApplicationContextNorth applicationContextNorth)
        {
            _applicationContextNorth = applicationContextNorth;
        }
        public async Task CreateNewAddress(AddressNorth address)
        {
            _applicationContextNorth.Addresses.Add(address);
            await _applicationContextNorth.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(AddressNorth address)
        {
            _applicationContextNorth.Addresses.Remove(address);
            await _applicationContextNorth.SaveChangesAsync();
        }

        public async Task<AddressNorth?> GetAddressByObjectIdAsync(Guid objectId)
        {
            return await _applicationContextNorth.Addresses.AsNoTracking()
              .FirstOrDefaultAsync(ca => (ca.EmployeeId == objectId
                                      || ca.BranchId == objectId
                                      || ca.CustomerId == objectId)
                                      && ca.IsDefault);
        }

        public Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCityNameByCityIdAsync(Guid cityId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDistrictNameByDistrictIdAsync(Guid districtId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRegionIdByCityIdAsync(Guid cityId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRegionNameByRegionIdAsync(string regionId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAddress(AddressNorth address)
        {
            _applicationContextNorth.Addresses.Update(address);
            await _applicationContextNorth.SaveChangesAsync();
        }
    }
}
