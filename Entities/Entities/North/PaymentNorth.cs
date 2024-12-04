using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.North
{
    public class PaymentNorth
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ICollection<OrderNorth>? Orders { get; set; }
    }
}
