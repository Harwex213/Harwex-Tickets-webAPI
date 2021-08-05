using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}