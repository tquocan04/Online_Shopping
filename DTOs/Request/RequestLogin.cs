using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestLogin
    {
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
