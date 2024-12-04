using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities.South
{
    public class BranchSouth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public ICollection<Branch_ProductSouth>? Branch_Products { get; set; }
        public ICollection<EmployeeSouth>? Employees { get; set; }
        [JsonIgnore]
        public ICollection<AddressSouth>? Addresses { get; set; }
    }
}
