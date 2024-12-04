
namespace Entities.Entities.South
{
    public class CategorySouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductSouth>? Products { get; set; }
    }
}
