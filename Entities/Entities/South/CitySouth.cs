using System.ComponentModel.DataAnnotations.Schema;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class CitySouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey(nameof(Region))]
        public string RegionId { get; set; } = null!;
        public RegionSouth? Region { get; set; }
        public ICollection<DistrictSouth>? Districts { get; set; }
    }
}
