using System.Data.Entity;
using DatabaseSchemaReader.Integration.Tests.Utils.Schema;

namespace DatabaseSchemaReader.Integration.Tests.Utils
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogContext")
        {
        }

        public DbSet<Blog> Blogs        { get; set; }
        public DbSet<Post> Posts        { get; set; }
        public DbSet<Author> Authors    { get; set; }
        public DbSet<Comment> Comments  { get; set; }
    }
}