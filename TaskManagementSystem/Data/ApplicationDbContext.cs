using Microsoft.EntityFrameworkCore;
// using TaskManagementSystem.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<TaskItem> Tasks { get; set; }
}
