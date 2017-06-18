using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            Property(i => i.Name)
            .HasMaxLength(50);

            Property(i => i.Size)
            .IsRequired();

            Property(i => i.Content)
            .IsRequired();

            Property(i => i.Type)
            .IsRequired()
            .HasMaxLength(10);
        }
    }
}
