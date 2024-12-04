using System.ComponentModel.DataAnnotations;
using Entities.Entities.South;


namespace Entities.Entities.South
{
    public class PaymentSouth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ICollection<OrderSouth>? Orders { get; set; }

    }
}
