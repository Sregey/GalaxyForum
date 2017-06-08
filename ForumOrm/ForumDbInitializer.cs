using System;
using System.Data.Entity;

namespace ForumOrm
{
    public class ForumDbInitializer : //DropCreateDatabaseIfModelChanges<ForumDbContext>
        DropCreateDatabaseAlways<ForumDbContext>
        //CreateDatabaseIfNotExists<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "Admin" });
            context.Roles.Add(new Role { Id = 2, Name = "Moderator" });
            context.Roles.Add(new Role { Id = 3, Name = "User" });

            context.Users.Add(new User()
            {
                Login = "User 1",
                Email = "mail@Mail.ru",
                RegisrationDate = DateTime.Now,
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 2,
                Login = "User 2",
                Email = "mail@Mail.ru",
                RegisrationDate = DateTime.Now,
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 3,
                Login = "User 3",
                Email = "mail@Mail.ru",
                RegisrationDate = DateTime.Now,
                RoleId = 1
            });
            context.Users.Add(new User()
            {
                Id = 4,
                Login = "User 4",
                Email = "mail@Mail.ru",
                RegisrationDate = DateTime.Now,
                RoleId = 1
            });

            base.Seed(context);
        }
    }
}
