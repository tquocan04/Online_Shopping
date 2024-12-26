using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICredentialRepo
    {
        Task CreateCredentialAsync(Credential credential);
    }
}
