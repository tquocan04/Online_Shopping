using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities.North
{
    public class OrderNorth
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCart { get; set; } = true;
        [JsonIgnore]
        public ICollection<ItemNorth>? Items { get; set; }
        [ForeignKey(nameof(Payment))]
        public string? PaymentId { get; set; }
        public PaymentNorth? Payment { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherId { get; set; }
        public VoucherNorth? Voucher { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; }
        public CustomerNorth? Customer { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public string? ShippingMethodId { get; set; }
        public ShippingMethodNorth? ShippingMethod { get; set; }
    }
}
