using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ICommentService
    {
        Task<Guid> CreateCommentAsync(CommentModel model);
    }
}
