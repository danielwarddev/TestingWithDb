using Microsoft.EntityFrameworkCore;
using TestingWithDb.Domain.AggregatesModel;
using TestingWithDb.Infrastructure.EntityConfigs;

namespace TestingWithDb.Infrastructure;

public class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductFavorite> ProductFavorites { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseCollation("utf8mb4_0900_ai_ci");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductMap).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductReview).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductFavorite).Assembly);
    }
}