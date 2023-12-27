using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace JWTREF.Model
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
            .HasOne(ur => ur.UserRefreshToken)
            .WithOne(u => u.User)
            .HasForeignKey<UserRefreshTokens> (ur => ur.Username);       
        }


        public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }
        public virtual DbSet<Users> User { get; set; }

    }
}
