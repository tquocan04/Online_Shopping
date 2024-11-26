using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(RequestLogin requestLogin);
    }
}
