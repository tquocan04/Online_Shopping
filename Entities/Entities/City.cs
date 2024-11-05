namespace Entities.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<District>? Districts { get; set; }
    }
}
