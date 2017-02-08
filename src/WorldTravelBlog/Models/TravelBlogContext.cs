using Microsoft.EntityFrameworkCore;

namespace WorldTravelBlog.Models
{
    public class TravelBlogContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TravelBlog;integrated security=True");
        }
    }
}
