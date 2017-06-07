namespace ForumOrm
{
    using System.Data.Entity;

    public class ForumDbContext : DbContext
    {
        public ForumDbContext() : base("name=DbGalaxyForum")
        {
            Database.SetInitializer(new ForumDbInitializer());
        }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);
        }*/
    }
}
