using DTOs.DTOs;
using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherDTO> CreateNewVoucher(RequestVoucher requestVoucher);
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
        Task<VoucherDTO> GetDetailVoucher(Guid id);
    }
}
