namespace DataAccessLayer.Entities
{
    public class Vote : BaseEntity
    {
        public DateTime? DateVote { get; set; } = DateTime.Now;
        public Guid? UserId { get; set; }

        public virtual User User { get; set; }
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
