﻿using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateOnly Dob { get; set; }
        public string? Image { get; set; }
        public string RegionId { get; set; } = null!;
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; } = null!;
    }
}
