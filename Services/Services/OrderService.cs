using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Runtime.InteropServices;

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

        public async Task AddToCart(Guid cusId, Guid prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var cartId = cart.Id;
            var product = await _productRepo.GetProductByIdAsync(prodId);
            var existingItem = await _orderRepo.GetItem(cartId, prodId);

            if (existingItem == null)
            {
                Item item = new Item
                {
                    OrderId = cartId,
                    ProductId = prodId,
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
            product.Stock--;
            await _productRepo.UpdateInforProduct(product);
        }


        public async Task<Order> CreateNewCart(Guid cusId)
        {
            Order order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = cusId,
                TotalPrice = 0,
            };
            await _orderRepo.CreateOrder(order);
            return order;
        }

        public async Task DeleteItemInCart(Guid cusId, Guid prodId)
        {
            Console.WriteLine($"CUSTOMER: {cusId}");

            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var product = await _productRepo.GetProductByIdAsync(prodId);
            Console.WriteLine($"PRODUCTTTTT: {prodId}");
            Item item = await _orderRepo.GetItem(cart.Id, product.Id);
            Console.WriteLine($"ITEMMMMMM: {prodId}");

            await _orderRepo.DeleteItemInCart(item);
            Console.WriteLine($"DELETEEEEEEE: {prodId}");


            cart.TotalPrice -= (decimal)(product.Price * item.Quantity);
            await _orderRepo.UpdateTotalPriceCart(cart.Id, cart.TotalPrice);
            Console.WriteLine($"UPDATEEEEEEE: {prodId}");

            product.Stock += item.Quantity;
            await _productRepo.UpdateInforProduct(product);
            Console.WriteLine($"UPDATE2222222222222: {prodId}");

        }


        public async Task<OrderCartDTO> GetOrderCart(Guid cusId)
        {

            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var cartDTO = _mapper.Map<OrderCartDTO>(cart);

            if (cartDTO.Items != null)
            {
                foreach (var item in cartDTO.Items)
                {
                    var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                    _mapper.Map(product, item);
                }
            }
            
            return cartDTO;
        }
    }
}
