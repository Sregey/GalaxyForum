using System.Data.Entity;
using ForumOrm.Models;
using ForumOrm.Configurations;

namespace ForumOrm
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext() : base("name=DbGalaxyForum")
        {
            Database.SetInitializer(new ForumDbInitializer());
            //Database.Initialize(true);
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new SectionConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
