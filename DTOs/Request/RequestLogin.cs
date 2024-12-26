using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Request
{
    public class RequestLogin
    {
        public string Login { get; set; } = null!;
        public string? Password { get; set; }
    }
}
