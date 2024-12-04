using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.North
{
    public class CredentialNorth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Provider { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public CustomerNorth Customer { get; set; }
    }
}
