using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IShippingRepo
    {
        Task<IEnumerable<ShippingMethod>> GetAllShippingMethods();
    }
}
