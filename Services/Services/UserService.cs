using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;

        public UserService
            (IUserRepo userRepo, 
            IMapper mapper, 
            IAddressRepo addressRepo) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
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

        public async Task<bool> UpdateInforUser(string id, string districtId, RequestCustomer requestUser)
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
                Guid.Parse(districtId),
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
                    DistrictId = Guid.Parse(districtId),
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
            user.Dob = dob;
            await _userRepo.UpdateInforCustomer(user);

            return true;
        }

        public async Task<CustomerDTO> GetProfileUser(string cusId)
        {
            var districtId = await _userRepo.GetDistrictDefaultByCustomerIdAsync(Guid.Parse(cusId));
            
            var cus = await _userRepo.GetCustomerByIdAsync(Guid.Parse(cusId));

            CustomerDTO cusDTO = _mapper.Map<CustomerDTO>(cus);

            cusDTO.Street = await _userRepo.GetStreetDefaultByCustomerIdAsync(Guid.Parse(cusId));
            cusDTO.CityId = await _addressRepo.GetCityIdByDistrictIdAsync(districtId);
            cusDTO.RegionId = await _addressRepo.GetRegionIdByCityIdAsync(cusDTO.CityId);
            cusDTO.DistrictId = districtId;

            return cusDTO;
            
        }
    }
}
