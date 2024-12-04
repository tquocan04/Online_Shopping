using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.North
{
    public class CityNorth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey(nameof(Region))]
        public string RegionId { get; set; } = null!;
        public RegionNorth? Region { get; set; }
        public ICollection<DistrictNorth>? Districts { get; set; }
    }
}
