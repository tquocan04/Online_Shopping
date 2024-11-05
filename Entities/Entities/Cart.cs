using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Cart
    {
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

    }
}
