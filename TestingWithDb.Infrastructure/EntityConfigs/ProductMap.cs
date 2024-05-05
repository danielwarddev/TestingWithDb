using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.EntityConfigs;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("product_pk");

        entity.ToTable("product");

        entity.Property(e => e.Id)
            .HasColumnName("id");
        entity.Property(e => e.Name)
            .HasColumnName("name");
    }
}