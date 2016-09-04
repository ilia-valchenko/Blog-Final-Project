using System.Data.Entity;
using ORM.Models;
using System.Web.Helpers;

namespace ORM
{
    public class DbInitializer : CreateDatabaseIfNotExists<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {
            #region Roles
            var userRole = new Role
            {
                Name = "user"
            };

            var adminRole = new Role
            {
                Name = "admin"
            };

            context.Roles.Add(userRole);
            context.Roles.Add(adminRole);
            #endregion

            #region Users
            var admin = new User
            {
                Nickname = "Ilia Valchenko",
                Email = "ilia.valchenko@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
            };

            admin.Roles.Add(userRole);
            admin.Roles.Add(adminRole);

            var user = new User
            {
                Nickname = "Izia Katzman",
                Email = "joseph.katzman@gmail.com",
                Password = Crypto.HashPassword("qwerty")
            };

            user.Roles.Add(userRole);

            context.Users.Add(admin);
            context.Users.Add(user);
            #endregion

            context.SaveChanges();
        }
    }
}
