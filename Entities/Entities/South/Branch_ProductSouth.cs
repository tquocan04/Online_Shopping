using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.South
{
    public class Branch_ProductSouth
    {
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public BranchSouth? Branch { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductSouth? Product { get; set; }
        public int? Quantity { get; set; }
    }
}
