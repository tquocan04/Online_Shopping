namespace Entities.Entities
{
    public class Import_Bill
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateTime Order_date { get; set; }
        public ICollection<Import_Product>? ImportProducts { get; set; }
    }
}
