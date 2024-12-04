using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.Center
{
    public class CredentialCenter
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Provider { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public CustomerCenter Customer { get; set; }
    }
}
