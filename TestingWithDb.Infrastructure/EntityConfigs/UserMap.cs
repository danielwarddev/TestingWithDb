using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.EntityConfigs;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Id).HasName("user_pk");

        entity.ToTable("user");

        entity.Property(e => e.Id)
            .HasColumnName("id");
        entity.Property(e => e.Name)
            .HasColumnName("name");
    }
}