﻿namespace Entities.Entities.Center
{
    public class VoucherCenter
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? Percentage { get; set; }
        public decimal? MaxDiscount { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public decimal? MinOrderValue { get; set; }
        public int? Quantity { get; set; }
        public ICollection<OrderCenter>? Orders { get; set; }
    }
}