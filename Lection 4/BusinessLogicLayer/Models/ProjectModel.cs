namespace BusinessLogicLayer.Models
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CreatorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
