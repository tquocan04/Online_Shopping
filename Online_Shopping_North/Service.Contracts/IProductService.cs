using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IProductService
    {
        Task<Product> CreateNewProduct(Product product);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetProductsNotHidden();
        Task<IEnumerable<ProductDTO>> GetProductsHidden();
        Task<ProductDTO> GetProductById(string id);
        Task UpdatestatusProduct(string id);
        Task UpdateInforProduct(Product requestProduct);
        Task DeleteProduct(string id);
    }
}
