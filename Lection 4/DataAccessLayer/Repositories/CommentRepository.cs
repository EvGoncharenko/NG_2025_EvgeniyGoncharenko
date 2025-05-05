using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : Repository<Comment> ,ICommentRepository
    {
        private readonly CrowdfundingDbContext _ctx;

        public CommentRepository(CrowdfundingDbContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }

        public async Task<Comment> GetCommentByIdWithUserAsync(Guid Id)
        {
            var commet = _ctx.Set<Comment>()
                .Include(x => x.User)
                .First(x => x.Id.Equals(Id));
            return commet;
        }

        public async Task<Comment> GetCommentByIdWithProjectAsync(Guid Id)
        {
            var comment = _ctx.Set<Comment>()
                .Include(x => x.Project)
                .First(x=> x.Id.Equals(Id));
            return comment;
        }
    }
}
