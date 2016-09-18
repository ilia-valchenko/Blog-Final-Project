using System.Data.Entity;
using ORM.Models;

namespace ORM
{
    /// <summary>
    /// A DbContext instance represents a combination of the Unit Of Work and Repository patterns such 
    /// that it can be used to query from a database and group together changes that will then 
    /// be written back to the store as a unit.
    /// </summary>
    public class BlogDbContext : DbContext
    {
        static BlogDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        /// <summary>
        /// Default constructor. 
        /// It refers to the base constructor, passing to it the connection string.
        /// </summary>
        public BlogDbContext() : base("name=Blog") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
