using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.Center
{
    public class Branch_ProductCenter
    {
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public BranchCenter? Branch { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public ProductCenter? Product { get; set; }
        public int? Quantity { get; set; }
    }
}
