using Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
