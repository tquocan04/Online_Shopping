using Entities.Entities.North;

namespace Repository.Contracts.Interfaces.North
{
    public interface IAddressNorthRepo
    {
        Task CreateNewAddress(AddressNorth address);
        Task<AddressNorth?> GetAddressByObjectIdAsync(Guid objectId);
        Task DeleteAddressAsync(AddressNorth address);
        Task UpdateAddress(AddressNorth address);
        Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId);
        Task<string> GetRegionIdByCityIdAsync(Guid cityId);
        Task<string> GetRegionNameByRegionIdAsync(string regionId);
        Task<string> GetCityNameByCityIdAsync(Guid cityId);
        Task<string> GetDistrictNameByDistrictIdAsync(Guid districtId);
    }
}
