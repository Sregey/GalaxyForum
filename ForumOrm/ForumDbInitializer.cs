using System;
using System.Data.Entity;

namespace ForumOrm
{
    public class ForumDbInitializer : DropCreateDatabaseAlways<ForumDbContext>// CreateDatabaseIfNotExists<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "Admin" });
            context.Roles.Add(new Role { Id = 2, Name = "Moderator" });
            context.Roles.Add(new Role { Id = 3, Name = "User" });

            context.Users.Add(new User()
            {
                Id = 1,
                Name = "User 1",
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 2,
                Name = "User 2",
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 3,
                Name = "User 3",
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 4,
                Name = "User 4",
                RoleId = 1
            });

            base.Seed(context);
        }
    }
}
