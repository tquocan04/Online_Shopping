using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities.Center
{
    public class OrderCenter
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCart { get; set; } = true;
        [JsonIgnore]
        public ICollection<ItemCenter>? Items { get; set; }
        [ForeignKey(nameof(Payment))]
        public string? PaymentId { get; set; }
        public PaymentCenter? Payment { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherId { get; set; }
        public VoucherCenter? Voucher { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; }
        public CustomerCenter? Customer { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public string? ShippingMethodId { get; set; }
        public ShippingMethodCenter? ShippingMethod { get; set; }
    }
}
