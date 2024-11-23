﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Range(9,11)]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public ICollection<Branch_Product>? Branch_Products { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        [JsonIgnore]
        public ICollection<Address>? Addresses { get; set; }
    }
}
