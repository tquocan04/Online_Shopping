using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IDistrictRepo
    {
        Task<IEnumerable<District>> GetDistrictsByCityIdAsync(Guid cityId);
    }
}
