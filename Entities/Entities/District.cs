using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class District
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Supplier>? Suppliers { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Cus_Address>? CusAddresses { get; set; }
        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public City? City { get; set; }
    }
}
