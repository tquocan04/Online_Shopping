using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Cus_Address
    {
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public District? District { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
