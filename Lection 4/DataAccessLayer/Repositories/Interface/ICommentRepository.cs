using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task<Comment> GetCommentByIdWithUserAsync(Guid id);
        public Task<Comment> GetCommentByIdWithProjectAsync(Guid id);
    }
}
