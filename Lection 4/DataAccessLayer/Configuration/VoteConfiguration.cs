using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        { 
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.User)
                .WithMany(v => v.Vote)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(p => p.Project)
                .WithMany(v => v.Vote)
                .HasForeignKey(p => p.ProjectId);
        }
    }
}
