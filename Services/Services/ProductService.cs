using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepo, IMapper mapper) 
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Product> CreateNewProduct(RequestProduct request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Service: Product cannot be null");
            }
            var product = new Product
            {
                Id = new Guid(), IsHidden = false,
            };
            
            _mapper.Map(request, product);
            await _productRepo.CreateNewProductAsync(product);
            return product;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsHidden()
        {
            var products = await _productRepo.GetProductsHiddenAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsNotHidden()
        {
            var products = await _productRepo.GetProductsNotHiddenAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task UpdateInforProduct(string id, RequestProduct requestProduct)
        {
            var productDTO = await GetProductById(id);
            if (productDTO == null)
            {
                throw new ArgumentException("Service: Product cannot be found");
            }
            Product product = new Product();
            _mapper.Map(requestProduct, productDTO);
            _mapper.Map(productDTO, product);
            await _productRepo.UpdateInforProduct(product);
        }

        public async Task UpdatestatusProduct(string id)
        {
            var productDTO = await GetProductById(id);
            if (productDTO == null)
            {
                throw new Exception("Service: Product cannot be found");
            }
            
            try
            {
                await _productRepo.UpdatestatusProduct(id);
            }
            catch
            {
                throw new Exception("Service: Product cannot be hidden");
            }
        }
    }
}
