using Gaming.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(p => new { p.DeviceId, p.GameId });
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category{CategoryId=1,Name="Sports"},
                    new Category{CategoryId=2,Name="Action"},
                    new Category{CategoryId=3,Name="Adventure"},
                    new Category{CategoryId=4,Name="Racing"},
                    new Category{CategoryId=5,Name="Fighting"},
                    new Category{CategoryId=6,Name="Film"}
                });
            modelBuilder.Entity<Device>()
                .HasData(new Device[]
                {
                    new Device{DeviceId=1,Name="Playstation",Icon="bi bi-playstation"},
                    new Device{DeviceId=2,Name="Xbox",Icon="bi bi-xbox"},
                    new Device{DeviceId=3,Name="Nintedo Switch",Icon="bi bi-nintendo-switch"},
                    new Device{DeviceId=4,Name="Pc",Icon="bi bi-pc-display"}
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
