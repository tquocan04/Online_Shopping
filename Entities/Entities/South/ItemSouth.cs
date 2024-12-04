using System.ComponentModel.DataAnnotations.Schema;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class ItemSouth
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public OrderSouth? Order { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductSouth? Product { get; set; }
        public int Quantity { get; set; } = 0;

    }
}
