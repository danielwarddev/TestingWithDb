using Microsoft.EntityFrameworkCore;

namespace TestingWithDb.Database;

public class ProductContext : DbContext
{
    public ProductContext() { }

    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<ProductFavorite> ProductFavorite { get; set; }

    public virtual DbSet<ProductReview> ProductReview { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pk");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProductFavorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_favorite_pk");

            entity.ToTable("product_favorite");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductFavorite)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_favorite_product_fk");

            entity.HasOne(d => d.User).WithMany(p => p.ProductFavorite)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_favorite_user_fk");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_review_pk");

            entity.ToTable("product_review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ReviewContent)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReview)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_review_product_fk");

            entity.HasOne(d => d.User).WithMany(p => p.ProductReview)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_review_user_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });
    }
}
