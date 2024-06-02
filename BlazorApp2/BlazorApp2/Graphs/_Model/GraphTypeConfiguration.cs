using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp2;

public class GraphTypeConfiguration : IEntityTypeConfiguration<Graph>
{
    public void Configure(EntityTypeBuilder<Graph> builder)
    {
        builder.ToTable("Graphs");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id);

        builder.Property(t => t.Name).IsRequired();
        
        builder
            .HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey("GraphId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}