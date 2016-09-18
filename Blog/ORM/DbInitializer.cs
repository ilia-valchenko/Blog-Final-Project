using System.Data.Entity;
using ORM.Models;
using System.Web.Helpers;

namespace ORM
{
    /// <summary>
    /// This is default initializer. It will create the database if none exists as per the configuration. 
    /// </summary>
    public class DbInitializer : CreateDatabaseIfNotExists<BlogDbContext>
    {
        /// <summary>
        /// This method seeds primary data to your database.
        /// </summary>
        /// <param name="context"></param>
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
                Nickname = "Ilia",
                Email = "i.valchenk@hotmail.com",
                Password = Crypto.HashPassword("1995-995"),
            };

            admin.Roles.Add(userRole);
            admin.Roles.Add(adminRole);

            var user = new User
            {
                Nickname = "katzman",
                Email = "joseph.katzman@gmail.com",
                Password = Crypto.HashPassword("1995-995")
            };

            user.Roles.Add(userRole);

            context.Users.Add(admin);
            context.Users.Add(user);
            #endregion

            context.SaveChanges();
        }
    }
}
