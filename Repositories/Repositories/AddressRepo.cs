﻿using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using System.IO;

namespace Repositories.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly ApplicationContext _applicationContext;

        public AddressRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task CreateNewAddress(Address address)
        {
            _applicationContext.Addresses.Add(address);
            await _applicationContext.SaveChangesAsync();
            _applicationContext.Entry(address).State = EntityState.Detached;
        }

        public async Task DeleteAddress(Address address)
        {
            _applicationContext.Addresses.Remove(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Address?> GetAddressByObjectIdAsync(Guid objectId)
        {
            return await _applicationContext.Addresses.AsNoTracking()
               .FirstOrDefaultAsync(ca => (ca.EmployeeId == objectId 
                                       || ca.BranchId == objectId
                                       || ca.CustomerId == objectId)
                                       && ca.IsDefault);
        }

        public async Task UpdateAddress(Address address)
        {
            _applicationContext.Addresses.Update(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId)
        {
            var result = await _applicationContext.Districts
                                        .Where(d => d.Id == districtId)
                                        .Select(d => d.CityId)
                                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetRegionIdByCityIdAsync(Guid cityId)
        {
            var result = await _applicationContext.Cities
                                        .Where(d => d.Id == cityId)
                                        .Select(d => d.RegionId)
                                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetRegionNameByRegionIdAsync(string regionId)
        {
            return await _applicationContext.Regions.AsNoTracking()
                .Where(r => r.Id == regionId)
                .Select(r => r.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetCityNameByCityIdAsync(Guid cityId)
        {
            return await _applicationContext.Cities.AsNoTracking()
                .Where(c => c.Id == cityId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetDistrictNameByDistrictIdAsync(Guid districtId)
        {
            return await _applicationContext.Districts.AsNoTracking()
                .Where(d => d.Id == districtId)
                .Select(d => d.Name)
                .FirstOrDefaultAsync();
        }

    }
}