﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EtAlii.Adp.Service;

public class GraphTypeConfiguration : IEntityTypeConfiguration<Graph>
{
    public void Configure(EntityTypeBuilder<Graph> builder)
    {
        builder.ToTable("Graphs");
        builder.Property(t => t.Name).IsRequired();
        
        builder
            .HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey("GraphId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}