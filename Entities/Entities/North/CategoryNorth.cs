namespace Entities.Entities.North
{
    public class CategoryNorth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductNorth>? Products { get; set; }
    }
}
