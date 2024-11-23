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
        private readonly IDistrictRepo _districtRepo;
        private readonly IMapper _mapper;
        private readonly ICityRepo _cityRepo;

        public UserService
            (IUserRepo userRepo, 
            IMapper mapper, 
            IDistrictRepo districtRepo,
            ICityRepo cityRepo) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _districtRepo = districtRepo;
            _cityRepo = cityRepo;
        }

        public async Task<Customer> CreateNewUser(RequestUser requestUser)
        {
            Customer user = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day)
            };
            _mapper.Map(requestUser, user);
            
            await _userRepo.CreateNewCustomer(user);
            return user;
        }

        private async Task UpdateAddress(string id)
        {
            var street = await _userRepo.GetStreetDefaultByCustomerIdAsync(Guid.Parse(id));
            var district = await _userRepo.GetDistrictDefaultByCustomerIdAsync(Guid.Parse(id));
            var old_address = await _userRepo.GetAddressByMultiPKAsync(Guid.Parse(id), district, street);
            old_address.IsDefault = false;
            await _userRepo.UpdateAddress(old_address);
        }

        public async Task<bool> UpdateInforUser(string id, string districtId, RequestUser requestUser)
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
            var address = await _userRepo.GetAddressByMultiPKAsync
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
                await _userRepo.CreateAddress(newAddress);
            }

            // 3.neu ton tai va false -> cap nhat lai = true
            if (address != null && !address.IsDefault)
            {
                // cap nhat record hien tai = false
                await UpdateAddress(id);

                // cap nhat lai = true
                address.IsDefault = true;
                await _userRepo.UpdateAddress(address);
            }



            DateOnly dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day);
            if (!await _userRepo.checkDOB(dob))
                throw new Exception("Dob is invalid");

            _mapper.Map(requestUser, user);
            user.Dob = dob;
            await _userRepo.UpdateInforCustomer(user);

            
            return true;
        }

        public async Task<CustomerDTO> GetProfileUser(string userId)
        {
            var districtId = await _userRepo.GetDistrictDefaultByCustomerIdAsync(Guid.Parse(userId));
            var district = await _districtRepo.GetDistrictsIdAsync(districtId);
            var city = await _cityRepo.GetCityByCityIdAsync(district.CityId);
            var street = await _userRepo.GetStreetDefaultByCustomerIdAsync(Guid.Parse(userId));
            var user = await _userRepo.GetCustomerByIdAsync(Guid.Parse(userId));

            CustomerDTO userDTO = new CustomerDTO();
            _mapper.Map(user, userDTO);
            userDTO.Street = street;
            userDTO.District = district.Name;
            userDTO.City = city.Name;
            return userDTO;
            
        }
    }
}
