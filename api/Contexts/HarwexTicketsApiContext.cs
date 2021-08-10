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
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaMovie> CinemaMovies { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }

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

            modelBuilder.Entity<Seat>(builder =>
            {
                builder.HasOne(d => d.SeatType)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.Type)
                    .HasPrincipalKey(t => t.Name);
            });
            
            modelBuilder.Entity<SeatType>(builder =>
            {
                builder.HasKey(s => s.Name);
            });
        }
    }
}