using InventoryManagementSystem.App.Entities;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.App.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<RoleFeature> RoleFeatures { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
