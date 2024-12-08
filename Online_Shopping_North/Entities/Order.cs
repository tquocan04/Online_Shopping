﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Online_Shopping_North.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCart { get; set; } = true;
        [JsonIgnore]
        public ICollection<Item>? Items { get; set; }
        [ForeignKey(nameof(Payment))]
        public string? PaymentId { get; set; }
        public Payment? Payment { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(ShippingMethod))]
        public string? ShippingMethodId { get; set; }
        public ShippingMethod? ShippingMethod { get; set; }
    }
}