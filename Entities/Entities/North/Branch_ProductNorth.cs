using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.North
{
    public class Branch_ProductNorth
    {
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public BranchNorth? Branch { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductNorth? Product { get; set; }
        public int? Quantity { get; set; }
    }
}
