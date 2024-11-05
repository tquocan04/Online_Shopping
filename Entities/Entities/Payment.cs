namespace Entities.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Bill>? Bills { get; set; }
    }
}
