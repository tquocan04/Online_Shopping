using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
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

        public async Task CreateNewCart(Guid orderId, Guid cusId)
        {
            Order order = new Order
            {
                Id = orderId,
                CustomerId = cusId,
                TotalPrice = 0,
            };
            
            await _orderRepo.CreateOrder(order);
        }

        public async Task DeleteItemInCart(Guid cusId, Guid prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(cusId);
            var product = await _productRepo.GetProductByIdAsync(prodId);

            Item item = await _orderRepo.GetItem(cart.Id, product.Id);
            await _orderRepo.DeleteItemInCart(item);

            cart.TotalPrice -= (decimal)(product.Price * item.Quantity);
            await _orderRepo.UpdateTotalPriceCart(cart.Id, cart.TotalPrice);

            product.Stock += item.Quantity;
            await _productRepo.UpdateInforProduct(product);
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
