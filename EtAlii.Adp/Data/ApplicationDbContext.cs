using EtAlii.Adp.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Dependency> Dependencies { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<Overview> Overviews { get; set; } = default!;
}