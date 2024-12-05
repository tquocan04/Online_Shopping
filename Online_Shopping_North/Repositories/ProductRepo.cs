using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationContext _applicationContext;

        public ProductRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        private async Task<bool> checkNameExist(Product product)
        {
            if (await _applicationContext.Products.AnyAsync(p => p.Name == product.Name))
                return true;
            return false;
        }

        public async Task CreateNewProductAsync(Product product)
        {
            if (await checkNameExist(product))
            {
                throw new Exception("Repo: This name is existed");
            }
            _applicationContext.Products.Add(product);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _applicationContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _applicationContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsHiddenAsync()
        {
            return await _applicationContext.Products.Where(p => p.IsHidden).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsNotHiddenAsync()
        {
            return await _applicationContext.Products.Where(p => !p.IsHidden).ToListAsync();
        }

        public async Task UpdatestatusProduct(Guid id)
        {
            var product = await _applicationContext.Products.FindAsync(id);
            if (product.IsHidden)
                product.IsHidden = false;
            else
                product.IsHidden = true;
            
            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateInforProduct(Product product)
        {
            _applicationContext.Products.Update(product);
            await _applicationContext.SaveChangesAsync();
            
        }

        public async Task DeleteProductAsync(Product product)
        {
            _applicationContext.Products.Remove(product);
            await _applicationContext.SaveChangesAsync();
        }

        public Task UpdateQuantityProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
