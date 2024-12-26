using Entities.Entities;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class CredentialRepo(ApplicationContext applicationContext) : ICredentialRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task CreateCredentialAsync(Credential credential)
        {
            _applicationContext.Credentials.Add(credential);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
