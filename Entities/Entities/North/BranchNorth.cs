using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities.North
{
    public class BranchNorth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public ICollection<Branch_ProductNorth>? Branch_Products { get; set; }
        public ICollection<EmployeeNorth>? Employees { get; set; }
        [JsonIgnore]
        public ICollection<AddressNorth>? Addresses { get; set; }
    }
}
