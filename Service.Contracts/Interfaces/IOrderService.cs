using DTOs.DTOs;

namespace Service.Contracts.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCartDTO> GetOrderCartAsync(string cusId);
        Task AddToCartAsync(string cusId, string prodId);
        Task DeleteItemInCartAsync(string cusId, string prodId);
    }
}
