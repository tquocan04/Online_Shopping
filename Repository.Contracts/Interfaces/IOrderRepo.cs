using DTOs.DTOs;
using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IOrderRepo
    {
        Task CreateOrder(Order order);
        Task UpdateTotalPriceCart(Guid orderId, decimal totalPrice);
        Task<Order> GetOrderIsCartByCusId(Guid cusId);
        Task<IEnumerable<ItemDTO>> GetListItemInCart(Guid cartId);
        Task<Guid> GetOrderIdByCusId(Guid cusId);
        Task AddItemToCart(Item item);
        Task UpdateQuantityItemToCart(Item item);
        Task DeleteItemInCart(Item item);
        Task<Item> GetItem(Guid orderId, Guid prodId);
    }
}