using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IProductRepo
    {
        Task CreateNewProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsNotHiddenAsync();
        Task<IEnumerable<Product>> GetProductsHiddenAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task UpdatestatusProduct(Product product);
        Task UpdateInforProduct(Product product);
        Task DeleteProductAsync(Product product);
    }
}
