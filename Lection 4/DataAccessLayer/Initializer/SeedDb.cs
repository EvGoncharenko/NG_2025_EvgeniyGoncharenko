using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Initializer
{
    public class SeedDb
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SeedDb(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void SeedLocalEnv()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();
            
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<CrowdfundingDbContext>())
                {
                    if (!context.Users.Any())
                    {
                        var user1 = new User
                        {
                            Name = "User1",
                            SecondName = "UserUser1"
                        };

                        var user2 = new User
                        {
                            Name = "User2",
                            SecondName = "UserUser2"
                        };

                        context.Users.AddRange(new[] { user1, user2 });
                    }

                    if (!context.Categories.Any())
                    {
                        var category1 = new Category
                        {
                            Description = $"{Guid.NewGuid()} description"
                        };

                        context.Categories.Add(category1);
                    }

                    context.SaveChanges();

                    if (!context.Projects.Any())
                    {
                        var users = context.Users.ToList();
                        var category = context.Categories.First().Id;

                        var project1 = new Project
                        {
                            Name = "Name1",
                            Description = "Description1",
                            CreatorId = users[0].Id,
                            CategoryId = category
                        };

                        var project2 = new Project
                        {
                            Name = "Name2",
                            Description = "Description2",
                            CreatorId = users[1].Id,
                            CategoryId = category
                        };

                        context.Projects.AddRange(new[] { project1, project2 });
                    }

                    
                    context.SaveChanges();

                }
            }

        }
    }
}
