using System.Reflection.Metadata.Ecma335;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly CrowdfundingDbContext _ctx;
        public ProjectRepository(CrowdfundingDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Project> GetProjectByIdWithAllInfoAsync(Guid Id)
        {
            var project = _ctx.Set<Project>()
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Include(x => x.Vote)
                .First(x => x.Id.Equals(Id));

            return project;
        }
    }
}
