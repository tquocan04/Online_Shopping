using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IBillService
    {
        Task<Order> CartToBill(Guid customerId, RequestBill requestBill);
        Task<List<OrderBillDTO>> GetOrderBill(Guid id);
        Task<List<OrderBillDTO>> GetListPendingBill();
        Task<List<OrderBillDTO>> GetListCompletedBill();
        Task<OrderBillDTO?> GetBillDetail(Guid orderId);
    }
}
