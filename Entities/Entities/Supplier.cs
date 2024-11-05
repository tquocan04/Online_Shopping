using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Import_Product>? ImportProducts { get; set; }
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public District? District { get; set; }

    }
}
