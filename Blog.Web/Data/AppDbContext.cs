using Blog.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Post>  Posts{ get; set; }
    }
}
