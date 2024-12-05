using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<BranchDTO> _addressService;
        private readonly IEmployeeRepo _employeeRepo;

        public BranchService(IBranchRepo branchRepo, IMapper mapper, IAddressRepo addressRepo,
            IEmployeeRepo employeeRepo,
            IAddressService<BranchDTO> addressService) 
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressService = addressService;
            _employeeRepo = employeeRepo;
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
                Id = Guid.NewGuid(),
            };

            _mapper.Map(requestBranch, branch);
            
            var branchDTO = _mapper.Map<BranchDTO>(await _branchRepo.AddNewBranchAsync(branch));

            Address address = new Address
            {
                Id = Guid.NewGuid(),
                BranchId = branch.Id,
                IsDefault = true,
            };
            _mapper.Map(requestBranch, address);
            
            await _addressRepo.CreateNewAddress(address);

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

            //var address = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));

            //await _addressRepo.DeleteAddress(address);
            await _branchRepo.DeleteBranchAsync(branch);
        }

        public async Task UpdateBranch(string id, RequestBranch requestBranch)
        {
            BranchDTO branchDTO = _mapper.Map<BranchDTO>(requestBranch);
            Branch branch = _mapper.Map<Branch>(branchDTO);
            branch.Id = Guid.Parse(id);

            var existingAddress = await _addressRepo.GetAddressByObjectIdAsync(Guid.Parse(id));

            //string existingRegion = await _addressRepo.GetRegionIdByCityIdAsync(await _addressRepo.GetCityIdByDistrictIdAsync(existingAddress.DistrictId));
            
            if (existingAddress != null)
            {
                if (existingAddress.DistrictId != requestBranch.DistrictId || existingAddress.Street != requestBranch.Street)
                {
                    _mapper.Map(requestBranch, existingAddress);
                    await _addressRepo.UpdateAddress(existingAddress);
                }
            }

            await _branchRepo.UpdateBranchAsync(branch);
        }
    }
}
