using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
