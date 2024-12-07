using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IVoucherRepo
    {
        Task CreateNewVoucherAsync(Voucher voucher);
        Task<bool> CheckCodeVoucher(string code);
        Task<IEnumerable<Voucher>> GetVoucherListAsync();
    }
}
