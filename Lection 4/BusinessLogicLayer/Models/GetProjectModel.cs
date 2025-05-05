namespace BusinessLogicLayer.Models
{
    public class GetProjectModel : ProjectModel
    {
        public DateTime? CreatedDate { get; set; }

        public int? VoteCaount { get; set; }

        public UserModel User { get; set; }
        public CategoryModel Category { get; set; }

        public List<CommentModel> Comments { get; set; }
    }
}
