using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime Dob { get; set; }
        public string? Image { get; set; }
        public ICollection<Cus_Address>? CusAddresses { get; set; }
        public Employee? Employee { get; set; }
        public Customer? Customer { get; set; }
    }
}
