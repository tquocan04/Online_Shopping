using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Import_Product
    {
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ImportBill))]
        public Guid ImportBillId { get; set; }
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }
        public Product? Product { get; set; }
        public Import_Bill? ImportBill { get; set; }
        public Supplier? Supplier { get; set; }
        public int Quantity { get; set; }
    }
}
