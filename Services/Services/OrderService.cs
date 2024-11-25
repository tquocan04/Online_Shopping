﻿using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public OrderService(IOrderRepo orderRepo, IMapper mapper, IProductRepo productRepo) 
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task AddToCartAsync(string cusId, string prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
            var cartId = cart.Id;
            var product = await _productRepo.GetProductByIdAsync(prodId);
            var existingItem = await _orderRepo.GetItem(cartId, Guid.Parse(prodId));

            if (existingItem == null)
            {
                Item item = new Item
                {
                    OrderId = cartId,
                    ProductId = Guid.Parse(prodId),
                    Quantity = 1
                };
                cart.TotalPrice += (decimal)product.Price;
                await _orderRepo.AddItemToCart(item);
            }
            else
            {
                existingItem.Quantity++;
                cart.TotalPrice += (decimal)product.Price;
                await _orderRepo.UpdateQuantityItemToCart(existingItem);
            }
            await _orderRepo.UpdateTotalPriceCart(cartId, cart.TotalPrice);
        }

        public async Task DeleteItemInCartAsync(string cusId, string prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
            var product = await _productRepo.GetProductByIdAsync(prodId);

            Item item = await _orderRepo.GetItem(cart.Id, product.Id);
            await _orderRepo.DeleteItemInCart(item);

            cart.TotalPrice -= (decimal)(product.Price * item.Quantity);
            await _orderRepo.UpdateTotalPriceCart(cart.Id, cart.TotalPrice);
        }

        public async Task<OrderCartDTO> GetOrderCartAsync(string cusId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
            return _mapper.Map<OrderCartDTO>(cart);
        }
    }
}
