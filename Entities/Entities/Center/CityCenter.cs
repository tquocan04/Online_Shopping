using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.Center
{
    public class CityCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey(nameof(Region))]
        public string RegionId { get; set; } = null!;
        public RegionCenter? Region { get; set; }
        public ICollection<DistrictCenter>? Districts { get; set; }
    }
}

}
