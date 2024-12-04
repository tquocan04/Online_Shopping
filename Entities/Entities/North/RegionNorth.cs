using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.North
{
    public class RegionNorth
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string? Name { get; set; }
        public ICollection<CityNorth>? Cities { get; set; }
    }
}
