﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class EmployeeSouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        public DateOnly Dob { get; set; }
        [JsonIgnore]
        public ICollection<AddressSouth>? Addresses { get; set; }
        [ForeignKey(nameof(Role))]
        public string RoleId { get; set; } = null!;
        public RoleSouth? Role { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public BranchSouth? Branch { get; set; }
    }
}
