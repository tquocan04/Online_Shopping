using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.Center
{
    public class RegionCenter
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string? Name { get; set; }
        public ICollection<CityCenter>? Cities { get; set; }
    }
}
