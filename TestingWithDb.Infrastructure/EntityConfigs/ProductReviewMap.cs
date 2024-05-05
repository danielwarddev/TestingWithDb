using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.EntityConfigs;

public class ProductReviewMap : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("product_review_pk");

        entity.ToTable("product_review");

        entity.Property(e => e.Id)
            .HasColumnName("id");
        entity.Property(e => e.ProductId)
            .HasColumnName("product_id");
        entity.Property(e => e.UserId)
            .HasColumnName("user_id");
        entity.Property(e => e.ReviewContent)
            .HasColumnName("name");

        entity
            .HasOne(d => d.Product)
            .WithMany(p => p.ProductReview)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("product_review_product_fk");

        entity
            .HasOne(d => d.User)
            .WithMany(p => p.ProductReview)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("product_review_user_fk");
    }
}