using System.Data.Entity;
using ForumOrm.Models;

namespace ForumOrm
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext() : base("name=DbGalaxyForum")
        {
            Database.SetInitializer(new ForumDbInitializer());
            Database.Initialize(true);
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
            modelBuilder.Entity<User>()
                .Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.RegisrationDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasColumnType("binary")
                .HasMaxLength(32);

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Status>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Status>()
                 .HasMany(s => s.Topics)
                 .WithRequired(t => t.Status)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Comment>()
                .Property(u => u.Text)
                .IsRequired();

            modelBuilder.Entity<Topic>()
                .Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Topic>()
                .Property(u => u.Text)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Topics)
                .WithRequired(t => t.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Image>()
                .Property(i => i.Size)
                .IsRequired();

            modelBuilder.Entity<Image>()
                .Property(i => i.Content)
                .IsRequired();

            modelBuilder.Entity<Image>()
                .Property(i => i.Type)
                .IsRequired();

            /*modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);*/
        }
    }
}
