using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public ICollection<Cus_Address>? CusAddresses { get; set; }
        public ICollection<Buy_Product>? BuyProducts { get; set; }
        public ICollection<Cus_Voucher>? CusVouchers { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
