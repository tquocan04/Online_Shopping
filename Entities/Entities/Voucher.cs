namespace Entities.Entities
{
    public class Voucher
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public int? Percentage { get; set; }
        public double? MaxDiscount { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public double? MinOrderValue { get; set; }
        public int? Quantity { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
