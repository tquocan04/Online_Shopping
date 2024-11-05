using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Buy_Product
    {
        [ForeignKey(nameof(Bill))]
        public Guid BillId { get; set; }
        public Bill? Bill { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int Quantity { get; set; }
    }
}
