using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // public DbSet<Graph> Graphs { get; init; } = null!;
    // public DbSet<Item> Items { get; init; } = null!;
    // public DbSet<Relation> Relations { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new GraphTypeConfiguration());
        // modelBuilder.ApplyConfiguration(new NodeTypeConfiguration());
    }
}