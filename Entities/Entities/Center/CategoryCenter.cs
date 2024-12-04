namespace Entities.Entities.Center
{
    public class CategoryCenter
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductCenter>? Products { get; set; }
    }
}
