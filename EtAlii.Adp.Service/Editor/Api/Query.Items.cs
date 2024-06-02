namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query
{
    public ItemsQuery Items { get; }
}

public class ItemsQuery 
{
    private readonly ILogger<ItemsQuery> _logger;

    public ItemsQuery(ILogger<ItemsQuery> logger)
    {
        _logger = logger;
    }

    public Task<Item> GetItem()
    {
        _logger.LogInformation("GraphQL {QueryName} query called", nameof(GetItem));

        var result = new Item { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> All([Service] DbContext context)
    {
        _logger.LogInformation("GraphQL {All} query called", nameof(GetItem));

        return context.Items;
    }
}