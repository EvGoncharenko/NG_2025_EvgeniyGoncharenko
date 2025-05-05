using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .HasMaxLength(256);

            builder.Property(x => x.Date)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(u => u.User)
                .WithMany(c => c.Comment)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(p => p.Project)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.ProjectId);
        }
    }
}
