using Microsoft.EntityFrameworkCore;
using ProdActivity.Entities;

namespace ProdActivity.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Root> Roots { get; set; }
        public DbSet<Detection> Detections { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.;Database=ProdActivity;Trusted_Connection=True;Encrypt=False");
            optionsBuilder.UseSqlServer(@"Server=localhost\\sqlserver,1433;Database=ProdActivity;User Id=sa;Password=Password1;Encrypt=False"); //Docker
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Detection>()
                        .Property(d => d.BBox)
                        .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<UserRole>().HasData(
    new UserRole { Id = 1, Name = "Yönetici" },
    new UserRole { Id = 2, Name = "Personel" });
            modelBuilder.Entity<User>().HasData(
    new User
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Name = "admin",
        Password = "1234",
        UserRoleId = 1,
        IsActive = true
    }
);

        }
    }
}
