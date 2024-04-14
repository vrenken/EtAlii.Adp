using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

public partial class DbContext
{
    public DbSet<Graph> Graphs { get; init; } = null!;
    public DbSet<Item> Items { get; init; } = null!;
    public DbSet<Relation> Relations { get; init; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GraphTypeConfiguration());
        // modelBuilder.ApplyConfiguration(new NodeTypeConfiguration());
    }
}