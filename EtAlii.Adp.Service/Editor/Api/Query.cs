using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query 
{
    public Task<View> GetNewView()
    {
        var result = new View { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }
}