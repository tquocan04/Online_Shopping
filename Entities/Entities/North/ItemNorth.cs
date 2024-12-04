using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.North
{
    public class ItemNorth
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public OrderNorth? Order { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductNorth? Product { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
