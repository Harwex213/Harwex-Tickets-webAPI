using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionSeatPrice> SessionSeatPrices { get; set; }
        public DbSet<SessionService> SessionServices { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserConfigure);

            modelBuilder.Entity<Seat>(SeatConfigure);
            modelBuilder.Entity<SeatType>(SeatTypeConfigure);
            
            modelBuilder.Entity<SessionSeatPrice>(SessionSeatPriceConfigure);
            modelBuilder.Entity<SessionService>(SessionServiceConfigure);
            
            modelBuilder.Entity<Service>(ServiceConfigure);
        }

        private void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(d => d.UserRoleNavigation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasPrincipalKey(t => t.Name)
                .HasConstraintName("FK_User_Role");
        }

        private void SeatConfigure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasOne(d => d.SeatTypeNavigation)
                .WithMany(p => p.Seats)
                .HasForeignKey(d => d.SeatType)
                .HasPrincipalKey(t => t.Name);
        }
        
        private void SeatTypeConfigure(EntityTypeBuilder<SeatType> builder)
        {
            builder.HasKey(s => s.Name);
        }
        
        private void SessionSeatPriceConfigure(EntityTypeBuilder<SessionSeatPrice> builder)
        {
            builder.Property(e => e.Price).HasColumnType("money");
                
            builder.HasOne(d => d.SeatTypeNavigation)
                .WithMany(p => p.SessionSeatPrices)
                .HasForeignKey(d => d.SeatType)
                .HasPrincipalKey(t => t.Name);
        }
        
        private void SessionServiceConfigure(EntityTypeBuilder<SessionService> builder)
        {
            builder.Property(e => e.Price).HasColumnType("money");
        }
        
        private void ServiceConfigure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Name);
        }
    }
}