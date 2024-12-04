namespace Entities.Entities.North
{
    public class RoleNorth
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public ICollection<EmployeeNorth>? Employees { get; set; }
    }
}
