using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Collections.Generic;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;

        public ProductService(IProductRepo productRepo, IMapper mapper, ICategoryRepo categoryRepo) 
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        private async Task<ProductDTO> ConvertToProductDTO(Product product)
        {
            var cate = await _categoryRepo.GetCategoryByIdAsync(product.CategoryId);

            var prodDTO = _mapper.Map<ProductDTO>(product);

            prodDTO.CategoryName = cate.Name;

            return prodDTO;
        }

        private async Task<IEnumerable<ProductDTO>> GetProducts(IEnumerable<Product> products)
        {
            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            var list = products.ToList();
            var listDTO = productDTO.ToList();

            for (int i = 0; i < listDTO.Count(); i++)
            {
                listDTO[i] = await ConvertToProductDTO(list[i]);
            }

            productDTO = listDTO;
            return productDTO;
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

            return await GetProducts(products);
        }

        public async Task<ProductDTO> GetProductById(string id)
        { 
            return await ConvertToProductDTO(await _productRepo.GetProductByIdAsync(Guid.Parse(id)));
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsHidden()
        {
            var products = await _productRepo.GetProductsHiddenAsync();
            return await GetProducts(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsNotHidden()
        {
            var products = await _productRepo.GetProductsNotHiddenAsync();
            return await GetProducts(products);
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
            
            await _productRepo.UpdatestatusProduct(Guid.Parse(id));
            
        }
    }
}
