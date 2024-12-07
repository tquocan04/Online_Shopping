using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IOrderService
    {
        Task CreateNewCart(Guid orderId, Guid cusId);
        Task<OrderCartDTO> GetOrderCart(Guid cusId);
        Task AddToCart(Guid cusId, Guid prodId);
        Task DeleteItemInCart(Guid cusId, Guid prodId);
    }
}
