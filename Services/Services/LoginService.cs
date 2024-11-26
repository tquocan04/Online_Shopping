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

        public async Task<string> LoginAsync(RequestLogin requestLogin)
        {
            if (requestLogin == null)
            {
                throw new ArgumentNullException("Service: Login cannot null");
            }

            if (requestLogin.Login.Contains("@"))
                return await _loginRepo.checkLoginCustomerAsync(requestLogin.Login, requestLogin.Password);
            else
                return await _loginRepo.checkLoginEmployeeAsync(requestLogin.Login, requestLogin.Password);

        }
    }
}
