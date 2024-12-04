using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.Center
{
    public class ItemCenter
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public OrderCenter? Order { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductCenter? Product { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
