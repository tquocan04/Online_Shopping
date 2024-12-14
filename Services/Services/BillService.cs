using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepo _billRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IVoucherRepo _voucherRepo;
        private readonly IOrderService _orderService;
        private readonly IProductRepo _productRepo;
        private readonly IAddressRepo _addressRepo;
        private readonly IMapper _mapper;

        public BillService(IBillRepo billRepo, IOrderRepo orderRepo,
            IVoucherRepo voucherRepo, IOrderService orderService,
            IProductRepo productRepo, IMapper mapper,
            IAddressRepo addressRepo) 
        {
            _billRepo = billRepo;
            _orderRepo = orderRepo;
            _voucherRepo = voucherRepo;
            _orderService = orderService;
            _productRepo = productRepo;
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        private async Task MapProductToItemDTO(OrderBillDTO orderBillDTO)
        {
            foreach (var item in orderBillDTO.Items)
            {
                var product = await _productRepo.GetProductByIdAsync(item.ProductId);
                _mapper.Map(product, item);
            }
        }
        public async Task<Order> CartToBill(Guid customerId, RequestBill requestBill)
        {
            Order order = await _orderRepo.GetOrderIsCartByCusId(customerId);

            order.IsCart = false;
            order.PaymentId = requestBill.PaymentId;
            order.ShippingMethodId = requestBill.ShippingMethodId;
            order.Status = "Pending";
            order.OrderDate = DateTime.Now;
            order.Note = requestBill.Note;

            if (requestBill.VoucherCode != null)
            {
                var voucherId = await _voucherRepo.GetVoucherIdByCode(requestBill.VoucherCode);
                Voucher voucher = await _voucherRepo.GetDetailVoucherByIdAsync(voucherId);

                bool checkVoucher = await _voucherRepo.IsValidVoucherById(voucherId,
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
                    order.VoucherId = voucherId;
                    voucher.Quantity -= 1;
                    await _voucherRepo.UpdateVoucherQuantityAsync(voucher.Id, voucher.Quantity);
                }
            }
            var addressDefault = await _addressRepo.GetAddressByObjectIdAsync(customerId);
            //var cityId = await _addressRepo.GetCityIdByDistrictIdAsync(addressDefault.DistrictId);
            //var regionId = await _addressRepo.GetRegionIdByCityIdAsync(cityId);

            if (requestBill.DistrictId != addressDefault.DistrictId
                    || requestBill.Street != addressDefault.Street)
            {
                List<Address> addresses = await _addressRepo.GetListAddressOfCustomerAsync(customerId);
                bool check = true;
                for (int i = 0; i < addresses.Count; i++)
                {
                    if (requestBill.DistrictId == addresses[i].DistrictId
                        && requestBill.Street == addresses[i].Street)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    Address address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = requestBill.Street,
                        DistrictId = requestBill.DistrictId,
                        CustomerId = customerId,
                        IsDefault = false,
                    };

                    await _addressRepo.CreateNewAddress(address);
                }
            }

            await _billRepo.CartToBillAsync(order);

            var newOrder = await _orderService.CreateNewCart(customerId);

            return order;
        }

        public async Task<OrderBillDTO?> GetBillDetail(Guid orderId)
        {
            Order? order = await _billRepo.GetBillDetailForEmployeeAsync(orderId);
            if (order == null)
                return null;

            OrderBillDTO orderBillDTO = _mapper.Map<OrderBillDTO>(order);

            await MapProductToItemDTO(orderBillDTO);

            return orderBillDTO;
        }

        public async Task<List<OrderBillDTO>> GetListCompletedBill()
        {
            List<Order> listBill = await _billRepo.GetListCompletedBillAsync();
            if (listBill.Count == 0)
                return null;

            List<OrderBillDTO> list = new List<OrderBillDTO>();

            for (int i = 0; i < listBill.Count; i++)
            {
                list.Add(_mapper.Map<OrderBillDTO>(listBill[i]));
                if (list[i].Items != null)
                    await MapProductToItemDTO(list[i]);
            }

            return list;
        }

        public async Task<List<OrderBillDTO>> GetListPendingBill()
        {
            List<Order> listBill = await _billRepo.GetListPendingBillAsync();
            if (listBill.Count == 0)
                return null;

            List<OrderBillDTO> list = new List<OrderBillDTO>();

            for (int i = 0; i < listBill.Count; i++)
            {
                list.Add(_mapper.Map<OrderBillDTO>(listBill[i]));
                if (list[i].Items != null)
                    await MapProductToItemDTO(list[i]);
            }

            return list;
        }

        public async Task<List<OrderBillDTO>> GetOrderBill(Guid id)
        {
            List<Order> listBill = await _billRepo.GetListBillForCustomerAsync(id);
            if (listBill.Count == 0)
                return null;

            List<OrderBillDTO> list = new List<OrderBillDTO>();

            for (int i = 0; i < listBill.Count; i++)
            {
                list.Add(_mapper.Map<OrderBillDTO>(listBill[i]));
                if (list[i].Items != null)
                    await MapProductToItemDTO(list[i]);
            }

            return list;
        }
    }
}
