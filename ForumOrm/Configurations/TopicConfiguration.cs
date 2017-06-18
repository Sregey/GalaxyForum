using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class TopicConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicConfiguration()
        {
            Property(u => u.Title)
            .IsRequired()
            .HasMaxLength(200);

            Property(u => u.Text)
            .IsRequired();

            //Property(t => t.Date)
            //.HasColumnType("datetime2")
            //.HasPrecision(0);

            //HasMany(t => t.Comments)
            //.WithRequired(c => c.Topic)
            //.WillCascadeOnDelete(true);

            HasRequired(t => t.Status)
            .WithMany(s => s.Topics)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.Section)
            .WithMany(s => s.Topics)
            .WillCascadeOnDelete(false);
        }
    }
}
