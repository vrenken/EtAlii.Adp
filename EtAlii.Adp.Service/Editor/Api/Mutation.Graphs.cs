using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Mutation 
{
    public async Task<Graph> AddGraph(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        string name)
    {
        var graph = new Graph { Name = name };
        await dbContext.Graphs.AddAsync(graph);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.GraphAdded), graph);
        
        return graph;
    }
    
    public async Task<Graph> ChangeGraph(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id, string name)
    {
        var graph = await dbContext.Graphs.SingleAsync(g => g.Id == id);
        graph.Name = name;
        dbContext.Graphs.Update(graph);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.GraphChanged), graph);
        
        return graph;
    }

    public async Task<Graph> RemoveGraph(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id)
    {
        var graph = await dbContext.Graphs.SingleAsync(g => g.Id == id);
        dbContext.Graphs.Remove(graph);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.GraphRemoved), graph);
        
        return graph;
    }

}