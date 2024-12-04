using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class ProductSouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Image { get; set; }
        public bool IsHidden { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public CategorySouth? Category { get; set; }
        [JsonIgnore]
        public ICollection<Branch_ProductSouth>? BranchProducts { get; set; }
        [JsonIgnore]
        public ICollection<ItemSouth>? Items { get; set; }
    }
}
