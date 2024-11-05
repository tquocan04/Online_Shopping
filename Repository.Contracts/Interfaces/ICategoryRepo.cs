namespace Repository.Contracts.Interfaces
{
    public interface ICategoryRepo
    {
        Task CreateNewCategoryAsync(string categoryName);
    }
}
