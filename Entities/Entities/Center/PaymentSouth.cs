using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.Center
{
    public class PaymentCenter
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ICollection<OrderCenter>? Orders { get; set; }
    }
}
