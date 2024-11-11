using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(3)]
        public string? Username { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? Phone_number { get; set; }
        public DateOnly Dob { get; set; }
        public string? Image { get; set; }
        [JsonIgnore]
        public ICollection<Cus_Address>? CusAddresses { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
