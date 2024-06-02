namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query 
{
    public Query(Graphs graphs, ItemsQuery itemsQuery)
    {
        Graphs = graphs;
        Items = itemsQuery;
    }
    public Graphs Graphs { get; }
}

public class Graphs 
{
    private readonly ILogger<Graphs> _logger;

    public Graphs(ILogger<Graphs> logger)
    {
        _logger = logger;
    }
    
    public Task<Graph> GetGraph()
    {
        _logger.LogInformation("GraphQL {QueryName} query called", nameof(GetGraph));

        var result = new Graph { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }

    // [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Graph> All([Service] DbContext context)
    {
        _logger.LogInformation("GraphQL {QueryName} query called", nameof(All));

        return context.Graphs;
    }
}