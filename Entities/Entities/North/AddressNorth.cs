using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.North
{
    public class AddressNorth
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; } = null;
        public CustomerNorth? Customer { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; } = null;
        public BranchNorth? Branch { get; set; }
        [ForeignKey(nameof(Employee))]
        public Guid? EmployeeId { get; set; } = null;
        public EmployeeNorth? Employee { get; set; }
        [ForeignKey(nameof(District))]
        public Guid DistrictId { get; set; }
        public DistrictNorth? District { get; set; }
        public string Street { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
