using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

            /*modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);*/
        }
    }
}
