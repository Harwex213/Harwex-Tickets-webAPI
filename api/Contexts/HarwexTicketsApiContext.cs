using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class HarwexTicketsApiContext : DbContext
    {
        public HarwexTicketsApiContext(DbContextOptions<HarwexTicketsApiContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .HasPrincipalKey(t => t.Name)
                    .HasConstraintName("FK_User_Role");
            });
        }
    }
}