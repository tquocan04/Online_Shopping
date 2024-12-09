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

        public async Task CreateNewVoucherAsync(Voucher voucher)
        {
            _applicationContext.Vouchers.Add(voucher);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Voucher>> GetVoucherListAsync()
        {
            return await _applicationContext.Vouchers
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _applicationContext.Vouchers.Update(voucher);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
