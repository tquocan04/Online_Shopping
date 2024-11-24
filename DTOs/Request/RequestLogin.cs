using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestLogin
    {
        [Required(ErrorMessage = "Phone number cannot null")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password cannot null")]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
