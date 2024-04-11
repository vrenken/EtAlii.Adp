using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<View> Views { get; set; }
}