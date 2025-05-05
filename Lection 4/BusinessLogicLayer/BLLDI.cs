using System.Net.NetworkInformation;
using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class BLLDI
    {
        public static void AddBLLLayer(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutomapperBLLProfile));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
