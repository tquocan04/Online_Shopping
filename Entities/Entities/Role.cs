namespace Entities.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
