using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class BranchDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
    }
}
