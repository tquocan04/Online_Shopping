using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IAddressRepo
    {
        Task CreateNewAddress(Address address);
        Task<Address?> GetAddressByMultiPKAsync(Guid objectId, Guid districtId, string Street);
        Task<Address?> GetAddressByObjectIdAsync(Guid objectId);
        Task DeleteAddress(Address address);
        Task UpdateAddress(Address address);
        Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId);
        Task<string> GetRegionIdByCityIdAsync(Guid cityId);
    }
}
