using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        private const int MAX_LENGTH = 50;

        public UserConfiguration()
        {
            Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(MAX_LENGTH);

            Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(MAX_LENGTH);

            Property(u => u.Name)
            .HasMaxLength(MAX_LENGTH);

            Property(u => u.LastName)
            .HasMaxLength(MAX_LENGTH);

            Property(u => u.FatherName)
            .HasMaxLength(MAX_LENGTH);

            Property(u => u.Profession)
            .HasMaxLength(MAX_LENGTH);

            //Property(u => u.RegisrationDate)
            //.HasColumnType("datetime2")
            //.HasPrecision(0);

            Property(u => u.Password)
            .IsRequired()
            .HasColumnType("binary")
            .HasMaxLength(32);

            HasRequired(u => u.Role)
            .WithMany()
            .WillCascadeOnDelete(false);

            HasRequired(u => u.Avatar)
            .WithMany()
            .WillCascadeOnDelete(false);
        }
    }
}
