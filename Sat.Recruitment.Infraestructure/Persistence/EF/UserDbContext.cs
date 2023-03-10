using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Infraestructure.Persistence.EF
{
    public class UserDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Email);
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnType("varchar(50)");
            modelBuilder.Entity<User>()
                .Property(u => u.Name).HasColumnType("varchar(50)");
            modelBuilder.Entity<User>()
                .Property(u => u.Address).HasColumnType("varchar(100)");
            modelBuilder.Entity<User>()
                .Property(u => u.Money).HasColumnType("decimal(19,4)");
            modelBuilder.Entity<User>()
                .Property(u => u.UserType).HasConversion<string>();
            modelBuilder.Entity<User>()
                .Property(u => u.Phone).HasColumnType("varchar(20)");
        }
    }
}
