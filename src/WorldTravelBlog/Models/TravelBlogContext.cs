using Microsoft.EntityFrameworkCore;

namespace WorldTravelBlog.Models
{
    public class TravelBlogContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ExperiencePerson> ExperiencePersons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TravelBlog;integrated security=True");
        }

        public TravelBlogContext()
        {
        }

        public TravelBlogContext(DbContextOptions<TravelBlogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
