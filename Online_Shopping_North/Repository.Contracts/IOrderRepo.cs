﻿using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
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
        Task DeleteOrderAsync(Guid id);
        Task<Item> GetItem(Guid orderId, Guid prodId);
    }
}
