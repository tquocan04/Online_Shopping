using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [Required]
        [MinLength(3)]
        public string? Username { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        
        [MaxLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits.")]
        public string? Phone_number { get; set; }
        //public DateOnly Dob { get; set; }
        
        public int Year {  get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string? Image { get; set; }
        [Required]
        public string Street { get; set; } = null!;
        
    }
}
