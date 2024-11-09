using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICityRepo
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<IEnumerable<District>> GetDistrictsByCityIdAsync(Guid cityId);
    }
}
