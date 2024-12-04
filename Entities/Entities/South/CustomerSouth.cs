using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class CustomerSouth
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
        public ICollection<AddressSouth>? Addresses { get; set; }
        [JsonIgnore]
        public ICollection<OrderSouth>? Orders { get; set; }
        public ICollection<CredentialSouth>? Credentials { get; set; }

    }
}
