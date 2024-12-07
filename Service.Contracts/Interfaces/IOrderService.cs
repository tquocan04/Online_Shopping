using DTOs.DTOs;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateNewCart(Guid cusId);
        Task<OrderCartDTO> GetOrderCart(string cusId);
        Task AddToCart(string cusId, string prodId);
        Task DeleteItemInCart(string cusId, string prodId);
    }
}
