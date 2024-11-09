using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateNewProduct(RequestProduct productDTO);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetProductsNotHidden();
        Task<IEnumerable<ProductDTO>> GetProductsHidden();
        Task<ProductDTO> GetProductById(string id);
        Task UpdatestatusProduct(string id);
        Task UpdateInforProduct(string id, RequestProduct requestProduct);

    }
}