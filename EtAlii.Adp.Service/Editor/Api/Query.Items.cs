namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query 
{
    public ItemsQuery Items => new();
}

public class ItemsQuery 
{
    public Task<Item> GetItem()
    {
        var result = new Item { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> All([Service] DbContext context)
    {
        return context.Items;
    }
}