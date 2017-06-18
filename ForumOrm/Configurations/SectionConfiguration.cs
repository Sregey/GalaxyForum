using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class SectionConfiguration : EntityTypeConfiguration<Section>
    {
        public SectionConfiguration()
        {
            Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);
        }
    }
}
