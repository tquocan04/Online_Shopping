namespace Entities.Entities
{
    public class Voucher
    {
        public Guid Id { get; set; }
        public Guid Code { get; set; }
        public int Percentage { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public double MinOrder { get; set;}
        public bool IsActive { get; set;}
        public ICollection<Cus_Voucher>? Cus_Vouchers { get; set; }
    }
}
