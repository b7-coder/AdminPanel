using Microsoft.EntityFrameworkCore;
using Авторизация.Models;

namespace Авторизация
{
    public class applicationContext : DbContext
    {
        public DbSet<Models.Users> users_persons { get; set; }
        public DbSet<Models.Images> images_table { get; set; }
        public applicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=avtoriz;Username=postgres;Password=12345");
        }
    }
}
