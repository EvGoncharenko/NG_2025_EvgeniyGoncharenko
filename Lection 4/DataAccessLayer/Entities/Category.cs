namespace DataAccessLayer.Entities
{
    public class Category : BaseEntity
    {
        public string? Description { get; set; }

        public List<Project> Project { get; set; }
    }
}
