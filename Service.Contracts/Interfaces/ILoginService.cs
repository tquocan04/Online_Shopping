using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(RequestLogin requestLogin);
    }
}
