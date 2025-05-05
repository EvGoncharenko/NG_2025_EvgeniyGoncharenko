
namespace BusinessLogicLayer.Models
{
    public class VoteModel : BaseModel
    {
        public DateTime? DateVote { get; set; }

        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
