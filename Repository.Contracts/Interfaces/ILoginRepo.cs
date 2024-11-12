namespace Repository.Contracts.Interfaces
{
    public interface ILoginRepo
    {
        Task<bool> checkLoginAsync(string email, string password);
        //Task<bool> checkPassword(string password);
    }
}
