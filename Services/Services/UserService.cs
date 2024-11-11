using AutoMapper;
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

        public UserService(IUserRepo userRepo, IMapper mapper) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<User> CreateNewUser(RequestUser requestUser)
        {
            User user = new User
            {
                Id = new Guid(),
                Dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day)
            };
            _mapper.Map(requestUser, user);
            
            await _userRepo.CreateNewUser(user);
            return user;
        }

        private async Task UpdateCusAddress(string id)
        {
            var street = await _userRepo.GetStreetDefaultByUserIdAsync(Guid.Parse(id));
            var district = await _userRepo.GetDistrictDefaultByUserIdAsync(Guid.Parse(id));
            var old_cus_address = await _userRepo.GetCusAddressByMultiPKAsync(Guid.Parse(id), district, street);
            old_cus_address.IsDefault = false;
            await _userRepo.UpdateCusAddress(old_cus_address);
        }

        public async Task<bool> UpdateInforUser(string id, string districtId, RequestUser requestUser)
        {
            var user = await _userRepo.GetUserByIdAsync(Guid.Parse(id));
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be found");
            }
            if (requestUser == null)
            {
                throw new ArgumentNullException("Information cannot be null");
            }


            // truy van tim record:
            var cus_address = await _userRepo.GetCusAddressByMultiPKAsync
                (
                Guid.Parse(id),
                Guid.Parse(districtId),
                requestUser.Street
                );
            // 1. neu ton tai -> khong cap nhat -> giu nguyen
            // 2. neu chua ton tai -> cap nhat record cu = false -> tao moi
            if (cus_address == null)
            {
                // cap nhat record cu
                await UpdateCusAddress(id);

                // tao moi
                Cus_Address newCusAdd = new Cus_Address
                {
                    UserId = Guid.Parse(id),
                    DistrictId = Guid.Parse(districtId),
                    Street = requestUser.Street,
                    IsDefault = true
                };
                await _userRepo.CreateCusAddress(newCusAdd);
            }

            // 3.neu ton tai va false -> cap nhat lai = true
            if (cus_address != null && !cus_address.IsDefault)
            {
                // cap nhat record hien tai = false
                await UpdateCusAddress(id);

                // cap nhat lai = true
                cus_address.IsDefault = true;
                await _userRepo.UpdateCusAddress(cus_address);
            }



            DateOnly dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day);
            if (!await _userRepo.checkDOB(dob))
                throw new Exception("Dob is invalid");

            _mapper.Map(requestUser, user);
            user.Dob = dob;
            await _userRepo.UpdateInforUser(user);

            
            return true;
        }
    }
}
