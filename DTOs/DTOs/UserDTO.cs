using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(3)]
        public string? Username { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [Range(9, 12)]
        public string? Phone_number { get; set; }
        public DateTime Dob { get; set; }
        public string? Image { get; set; }
    }
}
