using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTOs.DTOs;
using DTOs.MongoDb;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Contracts.Interfaces;
using Service.Contracts;
using Service.Contracts.Interfaces;


namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMetadataService _metadataService;
        private readonly Cloudinary _cloudinary;

        public ProductService(IProductRepo productRepo, IMapper mapper, ICategoryRepo categoryRepo,
            IMetadataService metadataService, Cloudinary cloudinary)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _metadataService = metadataService;
            _cloudinary = cloudinary;
        }

        private async Task<ProductDTO> ConvertToProductDTO(Product product)
        {
            var cate = await _categoryRepo.GetCategoryByIdAsync(product.CategoryId);

            var prodDTO = _mapper.Map<ProductDTO>(product);

            prodDTO.CategoryName = cate.Name;

            return prodDTO;
        }

        private async Task<ProductMetadata> ConvertProductToProductMetadata(Product product)
        {
            ProductDTO prodDTO = await ConvertToProductDTO(product);

            var prodMetadata = _mapper.Map<ProductMetadata>(prodDTO);
            prodMetadata.Id = prodDTO.Id.ToString();
            return prodMetadata;
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

        private async Task UploadImageProduct(Product product, IFormFile file)
        {
            var fileName = $"prodonlineshopping_{file.FileName}";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            // luu tam cua he thong C:\Users\[UserName]\AppData\Local\Temp
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // upload anh len Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, filePath),
                PublicId = $"prod_{Guid.NewGuid()}"
            };

            var uploadResult = _cloudinary.Upload(uploadParams);
            var imageUrl = uploadResult.SecureUrl.AbsoluteUri;

            // luu url
            product.Image = imageUrl;

            // xoa file tam sau khi upload
            System.IO.File.Delete(filePath);
        }


        public async Task<Product> CreateNewProduct(RequestProduct request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Service: Product cannot be null");
            }
            var product = new Product
            {
                Id = Guid.NewGuid(),
                IsHidden = false,
            };

            _mapper.Map(request, product);
            if (request.Image != null && request.Image.Length > 0)
            {
                await UploadImageProduct(product, request.Image);
            }

            await _productRepo.CreateNewProductAsync(product);

            var prodMetadata = await ConvertProductToProductMetadata(product);
            //await _metadataService.CreateProductMetadataAsync(prodMetadata);

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

        public async Task<Product> UpdateInforProduct(string id, RequestProduct requestProduct)
        {
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(id));
            if (product == null)
            {
                throw new ArgumentException("Service: Product cannot be found");
            }

            _mapper.Map(requestProduct, product);
            if (requestProduct.Image != null && requestProduct.Image.Length > 0)
            {
                await UploadImageProduct(product, requestProduct.Image);
            }

            await _productRepo.UpdateInforProduct(product);

            //await _metadataService.UpdateProductMetadataAsync(await ConvertProductToProductMetadata(product));

            return product;
        }

        public async Task UpdatestatusProduct(string id)
        {
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(id));
            if (product == null)
            {
                throw new Exception("Service: Product cannot be found");
            }

            await _productRepo.UpdatestatusProduct(product.Id);
        }

        public async Task DeleteProduct(string id)
        {
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(id));
            await _productRepo.DeleteProductAsync(product);

            //await _metadataService.DeleteProductMetadataAsync(await ConvertProductToProductMetadata(product));
        }
    }
}