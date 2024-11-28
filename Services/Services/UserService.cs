﻿using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<CustomerDTO> _addressService;
        private readonly Cloudinary _cloudinary;

        public UserService
            (IUserRepo userRepo, 
            IMapper mapper, 
            IAddressRepo addressRepo,
            IAddressService<CustomerDTO> addressService,
            Cloudinary cloudinary
            ) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressService = addressService;
            _cloudinary = cloudinary;
        }

        public async Task<Customer> CreateNewUser(Guid Id, RequestCustomer requestUser)
        {
            Customer customer = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day)
            };
            _mapper.Map(requestUser, customer);
            
            await _userRepo.CreateNewCustomer(customer);
            return customer;
        }

        private async Task UpdateAddress(string id)
        {
            var address = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));
            address.IsDefault = false;
            await _addressRepo.UpdateAddress(address);
        }

        public async Task<bool> UpdateInforUser(string id, RequestCustomer requestUser)
        {
            var user = await _userRepo.GetCustomerByIdAsync(Guid.Parse(id));
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be found");
            }
            if (requestUser == null)
            {
                throw new ArgumentNullException("Information cannot be null");
            }

            // truy van tim record:
            var address = await _addressRepo.GetAddressByMultiPKAsync
                (
                Guid.Parse(id),
                requestUser.DistrictId,
                requestUser.Street
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
                    DistrictId = requestUser.DistrictId,
                    Street = requestUser.Street,
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

            DateOnly dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day);
            if (!_userRepo.checkDOB(requestUser.Year))
                throw new Exception("Dob is invalid");

            _mapper.Map(requestUser, user);

            if (requestUser.Picture != null && requestUser.Picture.Length > 0)
            {
                await UploadImage(user, requestUser.Picture);
            }

            user.Dob = dob;
            await _userRepo.UpdateInforCustomer(user);

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
