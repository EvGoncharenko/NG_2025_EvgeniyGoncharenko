using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class VoteReposipotory : Repository<Vote>, IVoteRepository
    {
        private readonly CrowdfundingDbContext _ctx;

        public VoteReposipotory(CrowdfundingDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Vote> GetVoteByIdWithUserAsync(Guid Id)
        {
            var vote = _ctx.Set<Vote>()
                .Include(x => x.User)
                .First(x => x.Id.Equals(Id));
            return vote;
        }

        public async Task<Vote> GetVoteByIdWithProjectAsync(Guid Id)
        {
            var vote = _ctx.Set<Vote>()
                .Include(x => x.Project)
                .First(x => x.Id.Equals(Id));
            return vote;
        }
    }
}
