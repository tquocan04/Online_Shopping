using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Address
    {
        public Guid ObjectId { get; set; }

        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public District? District { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
