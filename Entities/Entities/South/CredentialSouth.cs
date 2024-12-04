using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class CredentialSouth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Provider { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public CustomerSouth Customer { get; set; }
    }
}
