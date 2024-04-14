namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Query 
{
    public RelationsQuery Relations => new();
}

public class RelationsQuery 
{
    public Task<Relation> GetRelation()
    {
        var result = new Relation { Id = Guid.NewGuid() };
        return Task.FromResult(result);
    }

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Relation> All([Service] DbContext context)
    {
        return context.Relations;
    }
}