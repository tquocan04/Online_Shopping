using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Cus_Voucher
    {
        [ForeignKey(nameof(Voucher))]
        public Guid VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set;}
        public Customer? Customer { get; set; }
        public bool IsUsed { get; set; }
    }
}
