using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.Center
{
    public class ShippingMethodCenter
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<OrderCenter>? Oders { get; set; }
    }
}
