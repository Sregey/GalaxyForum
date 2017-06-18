using ForumOrm.Models;
using System.Data.Entity.ModelConfiguration;

namespace ForumOrm.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(u => u.Text)
            .IsRequired();

            HasRequired(c => c.Status)
            .WithMany()
            .WillCascadeOnDelete(false);

            HasOptional(c => c.Sender)
            .WithMany()
            .WillCascadeOnDelete(false);

            HasRequired(c => c.Topic)
            .WithMany(t => t.Comments)
            .WillCascadeOnDelete(true);
        }
    }
}
