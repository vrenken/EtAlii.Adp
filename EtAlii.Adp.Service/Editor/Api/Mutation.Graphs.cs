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

        var startItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 50, W = 145, H = 60, Name = "Start" };
        graph.Items.Add(startItem);
        var initItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 140, W = 145, H = 60, Name = "Init" };
        graph.Items.Add(initItem);
        var conditionItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 230, W = 145, H = 60, Name = "Condition" };
        graph.Items.Add(conditionItem);
        var printItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 320, W = 145, H = 60, Name = "Print" };
        graph.Items.Add(printItem);
        var incrementItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 410, W = 145, H = 60, Name = "Increment" };
        graph.Items.Add(incrementItem);
        var endItem = new Item { Id = Guid.NewGuid(), X = 300, Y = 500, W = 145, H = 60, Name = "End" };
        graph.Items.Add(endItem);

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