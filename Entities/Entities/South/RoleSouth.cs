namespace Entities.Entities.South
{
    public class RoleSouth
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<EmployeeSouth>? Employees { get; set; }
    }
}
