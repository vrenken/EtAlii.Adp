namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query 
{
    public Graphs Graphs => new();
}

public class Graphs 
{
    public Task<Graph> GetGraph()
    {
        var result = new Graph { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }

    // [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Graph> All([Service] DbContext context)
    {
        return context.Graphs;
    }
}