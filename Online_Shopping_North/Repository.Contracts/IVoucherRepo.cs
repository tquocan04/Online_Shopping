using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface IVoucherRepo
    {
        Task CreateNewVoucherAsync(Voucher voucher);
        Task<IEnumerable<Voucher>> GetVoucherListAsync();
    }
}
