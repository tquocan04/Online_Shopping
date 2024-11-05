using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Bill
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateTime Order_date { get; set; }
        public string? Status { get; set; }
        public ICollection<Buy_Product>? BuyProducts { get; set; }
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment? Payment { get; set; }
    }
}
