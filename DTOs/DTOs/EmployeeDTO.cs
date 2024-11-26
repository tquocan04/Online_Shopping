using Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTOs.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateOnly Dob { get; set; }
        public string RoleId { get; set; } = null!;
        public Guid BranchId { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
    }
}
