namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Mutation 
{
    public async Task<Graph> AddGraph([Service] DbContext dbContext, Graph graph)
    {
        await dbContext.Graphs.AddAsync(graph);

        await dbContext.SaveChangesAsync();
        
        return graph;
    }
}