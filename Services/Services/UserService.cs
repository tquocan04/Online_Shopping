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
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressService<CustomerDTO> _addressService;
        private readonly IMetadataService _metadataService;
        private readonly Cloudinary _cloudinary;

        public UserService
            (IUserRepo userRepo, 
            IMapper mapper, 
            IAddressRepo addressRepo,
            IOrderRepo orderRepo,
            IAddressService<CustomerDTO> addressService,
            IMetadataService metadataService,
            Cloudinary cloudinary
            ) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _orderRepo = orderRepo;
            _addressService = addressService;
            _metadataService = metadataService;
            _cloudinary = cloudinary;

        }

        private async Task<CustomerMetadata> ConvertCustomerToCustomerMetadata(Customer customer)
        {
            CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(customer);
            cusDTO = await _addressService.SetAddress(cusDTO, cusDTO.Id);
            var cusMetadata = _mapper.Map<CustomerMetadata>(cusDTO);
            cusMetadata.Id = customer.Id.ToString();
            return cusMetadata;
        }


        public async Task<Customer> CreateNewUser(RequestCustomer requestCustomer)
        {
            Customer customer = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day)
            };

            // Xử lý ảnh và tải lên Cloudinary nếu có
            if (requestCustomer.Picture != null && requestCustomer.Picture.Length > 0)
            {
                await UploadImage(customer, requestCustomer.Picture);
            }

            _mapper.Map(requestCustomer, customer);
            await _userRepo.CreateNewCustomer(customer);

            Address address = new Address
            {
                ObjectId = customer.Id,
                IsDefault = true,
            };
            _mapper.Map(requestCustomer, address);
            await _addressRepo.CreateNewAddress(address);

            Order order = new Order
            {
                Id = new Guid(),
                CustomerId = customer.Id,
                TotalPrice = 0,
            };

            await _orderRepo.CreateOrder(order);

            
            await _metadataService.CreateCustomerMetadataAsync(await ConvertCustomerToCustomerMetadata(customer));
            return customer;
        }

        private async Task UpdateAddress(string id)
        {
            var address = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));
            address.IsDefault = false;
            await _addressRepo.UpdateAddress(address);
        }

        public async Task<bool> UpdateInforUser(string id, RequestCustomer requestCustomer)
        {
            var user = await _userRepo.GetCustomerByIdAsync(Guid.Parse(id));
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be found");
            }
            if (requestCustomer == null)
            {
                throw new ArgumentNullException("Information cannot be null");
            }

            DateOnly dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day);
            if (!_userRepo.checkDOB(requestCustomer.Year))
            {
                throw new Exception("Dob is invalid");
                return false;
            }

            if (!await _userRepo.checkEmailById(user.Id, requestCustomer.Email))
            {
                throw new Exception("Email is existed");
                return false;
            }

            // truy van tim record:
            var address = await _addressRepo.GetAddressByMultiPKAsync
                (
                Guid.Parse(id),
                requestCustomer.DistrictId,
                requestCustomer.Street
                );

            // 1. neu ton tai -> khong cap nhat -> giu nguyen
            // 2. neu chua ton tai -> cap nhat record cu = false -> tao moi
            if (address == null)
            {
                // cap nhat record cu
                await UpdateAddress(id);

                // tao moi
                Address newAddress = new Address
                {
                    ObjectId = Guid.Parse(id),
                    DistrictId = requestCustomer.DistrictId,
                    Street = requestCustomer.Street,
                    IsDefault = true
                };
                await _addressRepo.CreateNewAddress(newAddress);
            }

            // 3.neu ton tai va false -> cap nhat lai = true
            if (address != null && !address.IsDefault)
            {
                // cap nhat record hien tai = false
                await UpdateAddress(id);

                // cap nhat lai = true
                address.IsDefault = true;
                await _addressRepo.UpdateAddress(address);
            }


            _mapper.Map(requestCustomer, user);

            if (requestCustomer.Picture != null && requestCustomer.Picture.Length > 0)
            {
                await UploadImage(user, requestCustomer.Picture);
            }

            user.Dob = dob;
            await _userRepo.UpdateInforCustomer(user);

            await _metadataService.UpdateCustomerMetadataAsync(await ConvertCustomerToCustomerMetadata(user));

            return true;
        }

        public async Task<CustomerDTO> GetProfileUser(string cusId)
        {
            var cus = await _userRepo.GetCustomerByIdAsync(Guid.Parse(cusId));

            CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(cus);

            cusDTO = await _addressService.SetAddress(cusDTO, cusDTO.Id);
            
            return cusDTO;
            
        }

        public async Task UploadImage(Customer customer, IFormFile file)
        {
            var fileName = $"cusonlineshopping_{file.FileName}";
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
                PublicId = $"customer_{Guid.NewGuid()}"
            };

            var uploadResult = _cloudinary.Upload(uploadParams);
            var imageUrl = uploadResult.SecureUrl.AbsoluteUri;

            // luu url
            customer.Picture = imageUrl;

            // xoa file tam sau khi upload
            System.IO.File.Delete(filePath);
        }
    }
}
