
namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string SecondName { get; set; }

        public List<Project> Project { get; set; }
        public List<Comment> Comment { get; set; }

        public List <Vote> Vote { get; set; }
        
    }
}
