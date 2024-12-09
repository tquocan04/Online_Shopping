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
        private readonly IVoucherRepo _voucherRepo;

        public OrderService(IOrderRepo orderRepo, IMapper mapper, IProductRepo productRepo,
            IVoucherRepo voucherRepo) 
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _voucherRepo = voucherRepo;
        }

        public async Task AddToCart(string cusId, string prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
            var cartId = cart.Id;
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(prodId));
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
            product.Stock--;
            await _productRepo.UpdateInforProduct(product);
        }

        public async Task<Order> CartToBill(Guid customerId, string paymentId, string shippingId,
                                            string? voucherCode)
        {
            Order order = await _orderRepo.GetOrderIsCartByCusId(customerId);

            order.IsCart = false;
            order.PaymentId = paymentId;
            order.ShippingMethodId = shippingId;
            order.Status = "Completed";
            order.OrderDate = DateTime.Now;

            if (voucherCode != null)
            {
                order.VoucherId = await _voucherRepo.GetVoucherIdByCode(voucherCode);
                Voucher voucher = await _voucherRepo.GetDetailVoucherByIdAsync(order.VoucherId);

                bool checkVoucher = await _voucherRepo.IsValidVoucherById(order.VoucherId,
                                                                            DateOnly.FromDateTime(DateTime.Now),
                                                                            order.TotalPrice,
                                                                            voucher.Quantity);
                if (checkVoucher)
                {
                    decimal discount = (order.TotalPrice * voucher.Percentage / 100);
                    if (discount > voucher.MaxDiscount)
                    {
                        order.TotalPrice -= voucher.MaxDiscount;
                    }
                    else
                    {
                        order.TotalPrice -= discount;
                    }

                    voucher.Quantity -= 1;
                    await _voucherRepo.UpdateVoucherQuantityAsync(voucher.Id, voucher.Quantity);
                }
            }
            
            
            await _orderRepo.CartToBillAsync(order);
            
            var newOrder = CreateNewCart(customerId);

            return order;
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

        public async Task DeleteItemInCart(string cusId, string prodId)
        {
            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(prodId));

            Item item = await _orderRepo.GetItem(cart.Id, product.Id);
            await _orderRepo.DeleteItemInCart(item);

            cart.TotalPrice -= (decimal)(product.Price * item.Quantity);
            await _orderRepo.UpdateTotalPriceCart(cart.Id, cart.TotalPrice);

            product.Stock += item.Quantity;
            await _productRepo.UpdateInforProduct(product);
        }

        public async Task<List<OrderBillDTO>> GetOrderBill(Guid id)
        {
            List<Order> listBill = await _orderRepo.GetListBillAsync(id);
            if (listBill.Count == 0)
                return null;

            List<OrderBillDTO> list = new List<OrderBillDTO>();

            for (int i = 0; i < listBill.Count; i++)
            {
                list.Add(_mapper.Map<OrderBillDTO>(listBill[i]));
                if (list[i].Items != null)
                {
                    foreach (var item in list[i].Items)
                    {
                        var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                        _mapper.Map(product, item);
                    }
                }
            }
            
              
            return list;
        }

        public async Task<OrderCartDTO> GetOrderCart(string cusId)
        {

            var cart = await _orderRepo.GetOrderIsCartByCusId(Guid.Parse(cusId));
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
