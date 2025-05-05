using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Initializer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DataAccessLayerInjection
    {
        public static void AddDataAccessLayer(
            this IServiceCollection services ,
            IConfiguration configuration)
        {
            services.AddDbContext<CrowdfundingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserReposytory, UserRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IVoteRepository, VoteReposipotory>();
            services.AddSingleton<SeedDb>();
        }
    }
}
