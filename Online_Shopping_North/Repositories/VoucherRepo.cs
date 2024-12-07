using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class VoucherRepo : IVoucherRepo
    {
        private readonly ApplicationContext _applicationContext;

        public VoucherRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<bool> CheckCodeVoucher(string code)
        {
            return await _applicationContext.Vouchers
                .AnyAsync(v => v.Code.ToUpper() == code.ToUpper());
        }

        public async Task CreateNewVoucherAsync(Voucher voucher)
        {
            _applicationContext.Vouchers.Add(voucher);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
