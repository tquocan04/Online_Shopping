using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IBillRepo
    {
        Task CartToBillAsync(Order order);
        Task<List<Order>> GetListBillForCustomerAsync(Guid customerId);
        Task<Order?> GetBillDetailForEmployeeAsync(Guid orderId);
        Task<List<Order>> GetListPendingBillAsync();
        Task<List<Order>> GetListCompletedBillAsync();
        Task CompletedBillAsync(Guid id);
    }
}
