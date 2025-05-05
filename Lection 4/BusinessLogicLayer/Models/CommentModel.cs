
namespace BusinessLogicLayer.Models
{
    public class CommentModel : BaseModel
    {
        public string? Text { get; set; }
        public DateTime? Date { get; set; }

        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
