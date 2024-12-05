﻿using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface IProductRepo
    {
        Task CreateNewProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsNotHiddenAsync();
        Task<IEnumerable<Product>> GetProductsHiddenAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task UpdatestatusProduct(Guid id);
        Task UpdateInforProduct(Product product);
        Task UpdateQuantityProduct(Product product);
        Task DeleteProductAsync(Product product);
    }
}
