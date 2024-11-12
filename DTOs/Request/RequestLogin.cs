using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestLogin
    {
        [Required(ErrorMessage = "Email cannot null")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password cannot null")]
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
