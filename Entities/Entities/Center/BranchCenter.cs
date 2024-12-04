using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities.Center
{
    public class BranchCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Range(9, 11)]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public ICollection<Branch_ProductCenter>? Branch_Products { get; set; }
        public ICollection<EmployeeCenter>? Employees { get; set; }
        [JsonIgnore]
        public ICollection<AddressCenter>? Addresses { get; set; }
    }
}
