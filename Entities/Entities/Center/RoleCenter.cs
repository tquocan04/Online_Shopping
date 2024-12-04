namespace Entities.Entities.Center
{
    public class RoleCenter
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<EmployeeCenter>? Employees { get; set; }
    }
}
