using System.Data.Entity;
using ORM.Models;

namespace ORM
{
    public class BlogDbContext : DbContext
    {
        static BlogDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public BlogDbContext() : base("name=BlogDbContext") {}

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
