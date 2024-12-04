using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class OrderSouth
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCart { get; set; } = true;
        [JsonIgnore]
        public ICollection<ItemSouth>? Items { get; set; }
        [ForeignKey(nameof(Payment))]
        public string? PaymentId { get; set; }
        public PaymentSouth? Payment { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherId { get; set; }
        public VoucherSouth? Voucher { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; }
        public CustomerSouth? Customer { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public string? ShippingMethodId { get; set; }
        public ShippingMethodSouth? ShippingMethod { get; set; }
    }
}
