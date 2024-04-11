using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

public partial class DbContext
{
    public DbSet<Graph> Graphs { get; set; }
}