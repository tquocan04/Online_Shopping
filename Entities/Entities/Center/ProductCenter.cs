using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities.Center
{
    public class ProductCenter
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
        public CategoryCenter? Category { get; set; }
        [JsonIgnore]
        public ICollection<Branch_ProductCenter>? BranchProducts { get; set; }
        [JsonIgnore]
        public ICollection<ItemCenter>? Items { get; set; }
    }
}
