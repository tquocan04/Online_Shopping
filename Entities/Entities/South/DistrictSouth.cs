using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Entities.Entities.South;

namespace Entities.Entities.South
{
    public class DistrictSouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<AddressSouth>? Addresses { get; set; }
        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public CitySouth? City { get; set; }
    }
}
