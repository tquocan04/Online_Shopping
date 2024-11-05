using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Product
    {                                                                                        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public string? Image { get; set; }
        public bool IsHidden { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Import_Product>? ImportProducts { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Buy_Product>? BuyProducts { get; set; }
    }
}
