using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Entities.Entities
{
    public class Employee
    {
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
