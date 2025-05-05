using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserReposytory
    {
        private readonly CrowdfundingDbContext _ctx;
        public UserRepository(CrowdfundingDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
