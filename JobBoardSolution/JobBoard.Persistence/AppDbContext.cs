using Microsoft.EntityFrameworkCore;
using JobBoard.Domain.Entities;

namespace JobBoard.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().OwnsMany(u => u.RefreshTokens);
    }
}
