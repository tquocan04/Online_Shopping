using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities.Center
{
    public class DistrictCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<AddressCenter>? Addresses { get; set; }
        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public CityCenter? City { get; set; }
    }
}
