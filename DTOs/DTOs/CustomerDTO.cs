using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class CustomerDTO
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? Phone_number { get; set; }
        public DateOnly Dob { get; set; }
        public string? Image { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
    }
}
