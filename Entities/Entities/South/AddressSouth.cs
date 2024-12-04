using Entities.Entities.South;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.South
{
    public class AddressSouth
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; } = null;
        public CustomerSouth? Customer { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; } = null;
        public BranchSouth? Branch { get; set; }
        [ForeignKey(nameof(Employee))]
        public Guid? EmployeeId { get; set; } = null;
        public EmployeeSouth? Employee { get; set; }
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public DistrictSouth? District { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
