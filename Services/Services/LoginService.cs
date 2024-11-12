using DTOs.Request;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService(ILoginRepo loginRepo) 
        {
            _loginRepo = loginRepo;
        }

        public async Task<bool> LoginAsync(RequestLogin requestLogin)
        {
            if (requestLogin == null)
            {
                throw new ArgumentNullException("Service: Login cannot null");
            }
            return await _loginRepo.checkLoginAsync(requestLogin.Email, requestLogin.Password);
        }
    }
}
