using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Mutation 
{
    public async Task<Item> AddItem(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        [ID] Guid graphId,
        float xPosition,
        float yPosition,
        string name)
    {
        var item = new Item
        {
            Name = name,
            X = xPosition,
            Y = yPosition
        };

        var graph = await dbContext.Graphs.SingleAsync(g => g.Id == graphId);
        graph.Items.Add(item);

        await dbContext.Items.AddAsync(item);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.ItemAdded), item);
        
        return item;
    }
    
    public async Task<Item> ChangeItem(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id, string name)
    {
        var item = await dbContext.Items.SingleAsync(g => g.Id == id);
        item.Name = name;
        dbContext.Items.Update(item);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.ItemChanged), item);
        
        return item;
    }

    public async Task<Item> RemoveItem(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id)
    {
        var item = await dbContext.Items.SingleAsync(g => g.Id == id);
        dbContext.Items.Remove(item);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.ItemRemoved), item);
        
        return item;
    }

}