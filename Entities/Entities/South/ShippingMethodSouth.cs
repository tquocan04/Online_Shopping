using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.South
{
    public class ShippingMethodSouth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<OrderSouth>? Oders { get; set; }
    }
}
