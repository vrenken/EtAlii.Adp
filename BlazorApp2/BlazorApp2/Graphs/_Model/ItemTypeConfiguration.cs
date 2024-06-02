using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp2;

public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id);

        builder.Property(t => t.Name).IsRequired();
        
        builder.HasIndex(t => t.Name);
    }
}