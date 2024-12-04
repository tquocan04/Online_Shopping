using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.North
{
    public class ShippingMethodNorth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<OrderNorth>? Oders { get; set; }
    }
}
