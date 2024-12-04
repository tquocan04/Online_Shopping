using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities.Center
{
    public class CustomerCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        public DateOnly Dob { get; set; }
        public string? Picture { get; set; }
        [JsonIgnore]
        public ICollection<AddressCenter>? Addresses { get; set; }
        [JsonIgnore]
        public ICollection<OrderCenter>? Orders { get; set; }
        public ICollection<CredentialCenter>? Credentials { get; set; }
    }
}
