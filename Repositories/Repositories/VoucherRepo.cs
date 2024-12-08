using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
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

        public async Task<Voucher> GetDetailVoucherByIdAsync(Guid id)
        {
            return await _applicationContext.Vouchers.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Voucher>> GetVoucherListAsync()
        {
            return await _applicationContext.Vouchers
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
