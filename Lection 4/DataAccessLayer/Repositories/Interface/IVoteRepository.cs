using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IVoteRepository : IRepository<Vote>
    {
        public Task<Vote> GetVoteByIdWithUserAsync(Guid Id);
        public Task<Vote> GetVoteByIdWithProjectAsync(Guid Id);
    }
}
