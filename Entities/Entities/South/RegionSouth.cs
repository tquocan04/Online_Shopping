using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.South
{
    public class RegionSouth
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string? Name { get; set; }
        public ICollection<CitySouth>? Cities { get; set; }
    }
}
