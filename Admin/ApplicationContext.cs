using Microsoft.EntityFrameworkCore;

namespace Admin
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Models.Projects> us_projects { get; set; }
        public DbSet<Models.Workers> workers_persons { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=admin_panel;Username=postgres;Password=12345");
        }
    }
}
