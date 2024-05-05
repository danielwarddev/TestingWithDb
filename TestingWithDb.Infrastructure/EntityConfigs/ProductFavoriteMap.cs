using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.EntityConfigs;

public class ProductFavoriteMap : IEntityTypeConfiguration<ProductFavorite>
{
    public void Configure(EntityTypeBuilder<ProductFavorite> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("product_favorite_pk");

        entity.ToTable("product_favorite");

        entity.Property(e => e.Id)
            .HasColumnName("id");
        entity.Property(e => e.ProductId)
            .HasColumnName("product_id");
        entity.Property(e => e.UserId)
            .HasColumnName("user_id");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.ProductFavorite)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("product_favorite_product_fk");

        entity.HasOne(d => d.User)
            .WithMany(p => p.ProductFavorite)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("product_favorite_user_fk");
    }
}