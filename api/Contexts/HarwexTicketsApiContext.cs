using System.Linq;
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
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaMovie> CinemaMovies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionSeatPrice> SessionSeatPrices { get; set; }
        public DbSet<SessionService> SessionServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SessionSeatPrice>(SessionSeatPriceConfigure);
            modelBuilder.Entity<SessionService>(SessionServiceConfigure);
        }

        private void SessionSeatPriceConfigure(EntityTypeBuilder<SessionSeatPrice> builder)
        {
            builder.Property(e => e.Price).HasColumnType("money");
        }

        private void SessionServiceConfigure(EntityTypeBuilder<SessionService> builder)
        {
            builder.Property(e => e.Price).HasColumnType("money");
        }
    }
}