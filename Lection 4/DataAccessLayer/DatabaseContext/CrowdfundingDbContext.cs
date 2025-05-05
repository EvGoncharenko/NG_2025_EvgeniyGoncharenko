using DataAccessLayer.Entities;
using DataAccessLayer.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DatabaseContext
{
    public class CrowdfundingDbContext : DbContext
    {
        public CrowdfundingDbContext(DbContextOptions<CrowdfundingDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
