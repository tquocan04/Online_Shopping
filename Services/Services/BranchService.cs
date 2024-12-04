using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Entities.Entities.North;
using Repository.Contracts.Interfaces;
using Repository.Contracts.Interfaces.North;
using Service.Contracts.Interfaces;
using System.Net;

namespace Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IBranchNorthRepo _branchNorthRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressNorthRepo _addressNorthRepo;
        private readonly IAddressService<BranchDTO> _addressService;

        public BranchService(IBranchRepo branchRepo, IMapper mapper, IAddressRepo addressRepo,
            IBranchNorthRepo branchNorthRepo,
            IAddressNorthRepo addressNorthRepo,
            IAddressService<BranchDTO> addressService) 
        {
            _branchRepo = branchRepo;
            _branchNorthRepo = branchNorthRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressNorthRepo = addressNorthRepo;
            _addressService = addressService;
        }
        private async Task<BranchDTO> SetAddress(BranchDTO branchDTO)
        {
            var address = await _addressRepo.GetAddressByObjectIdAsync(branchDTO.Id);

            branchDTO.Street = address.Street;
            branchDTO.DistrictId = address.DistrictId;
            branchDTO.CityId = await _addressRepo.GetCityIdByDistrictIdAsync(branchDTO.DistrictId);
            branchDTO.RegionId = await _addressRepo.GetRegionIdByCityIdAsync(branchDTO.CityId);

            return branchDTO;
        }

        public async Task<BranchDTO> AddNewBranch(RequestBranch requestBranch)
        {
            Branch branch = new Branch
            {
                Id = new Guid()
            };

            _mapper.Map(requestBranch, branch);
            
            var branchDTO = _mapper.Map<BranchDTO>(await _branchRepo.AddNewBranchAsync(branch));

            Address address = new Address
            {
                Id = new Guid(),
                BranchId = branch.Id,
                IsDefault = true,
            };
            _mapper.Map(requestBranch, address);
            
            await _addressRepo.CreateNewAddress(address);

            if (requestBranch.RegionId == "Bac")
            {
                var branchNorth = _mapper.Map<BranchNorth>(branch);
                await _branchNorthRepo.AddNewBranchAsync(branchNorth);

                var addressNorth = _mapper.Map<AddressNorth>(address);
                await _addressNorthRepo.CreateNewAddress(addressNorth);
            }

            return branchDTO;
        }

        public async Task<BranchDTO> GetBranch(string id)
        {
            var branch = await _branchRepo.GetBranchAsync(Guid.Parse(id));
            
            var branchDTO = _mapper.Map<BranchDTO>(branch);

            branchDTO = await _addressService.SetAddress(branchDTO, branchDTO.Id);
            
            return branchDTO;
        }

        public async Task<List<BranchDTO>> GetBranchList()
        {
            var branchList = await _branchRepo.GetBranchListAsync();
            var branchDtoList = _mapper.Map<IEnumerable<BranchDTO>>(branchList);

            var list = branchDtoList.ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                list[i] = await _addressService.SetAddress(list[i], list[i].Id);
            }
            return list;
        }

        public async Task DeleteBranch(string id)
        {
            var branch = await _branchRepo.GetBranchAsync(Guid.Parse(id));

            var address = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));
            
            var addressNorth = _mapper.Map<AddressNorth>(address);
            await _addressNorthRepo.DeleteAddressAsync(addressNorth);

            var branchNorth = _mapper.Map<BranchNorth>(branch);
            await _branchNorthRepo.DeleteBranchAsync(branchNorth);

            
            await _addressRepo.DeleteAddress(address);
            await _branchRepo.DeleteBranchAsync(branch);
        }

        public async Task UpdateBranch(string id, RequestBranch requestBranch)
        {
            BranchDTO branchDTO = _mapper.Map<BranchDTO>(requestBranch);
            Branch branch = _mapper.Map<Branch>(branchDTO);
            branch.Id = Guid.Parse(id);

            var existingAddress = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));

            string existingRegion = await _addressRepo.GetRegionIdByCityIdAsync(await _addressRepo.GetCityIdByDistrictIdAsync(existingAddress.DistrictId));
            
            if (existingAddress != null)
            {
                if (existingAddress.DistrictId != requestBranch.DistrictId || existingAddress.Street != requestBranch.Street)
                {
                    _mapper.Map(requestBranch, existingAddress);
                    if (existingRegion == "Bac" && requestBranch.RegionId == "Bac")
                    {
                        AddressNorth addressNorth = await _addressNorthRepo.GetAddressByObjectIdAsync(Guid.Parse(id));
                        addressNorth.DistrictId = requestBranch.DistrictId;
                        addressNorth.Street = requestBranch.Street;
                        await _addressNorthRepo.UpdateAddress(addressNorth);

                        BranchNorth branchNorth = _mapper.Map<BranchNorth>(branch);
                        await _branchNorthRepo.UpdateBranchAsync(branchNorth);
                    }
                    _mapper.Map(requestBranch, existingAddress);
                    await _addressRepo.UpdateAddress(existingAddress);
                }
            }

            await _branchRepo.UpdateBranchAsync(branch);
        }
    }
}
