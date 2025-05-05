namespace DataAccessLayer.Entities
{
    public class Project : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;

        public Guid? CreatorId { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual User? User { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Vote> Vote { get; set; }
        
    }
}
