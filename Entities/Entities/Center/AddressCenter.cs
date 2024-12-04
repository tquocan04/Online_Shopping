using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.Center
{
    public class AddressCenter
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; } = null;
        public CustomerCenter? Customer { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; } = null;
        public BranchCenter? Branch { get; set; }
        [ForeignKey(nameof(Employee))]
        public Guid? EmployeeId { get; set; } = null;
        public EmployeeCenter? Employee { get; set; }
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public DistrictCenter? District { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
