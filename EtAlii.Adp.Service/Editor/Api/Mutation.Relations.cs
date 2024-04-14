using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Mutation 
{
    public async Task<Relation> AddRelation(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        string name)
    {
        var relation = new Relation { Name = name };
        await dbContext.Relations.AddAsync(relation);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.RelationAdded), relation);
        
        return relation;
    }
    
    public async Task<Relation> ChangeRelation(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id,
        string name)
    {
        var relation = await dbContext.Relations.SingleAsync(g => g.Id == id);
        relation.Name = name;
        dbContext.Relations.Update(relation);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.RelationChanged), relation);
        
        return relation;
    }

    public async Task<Relation> RemoveRelation(
        [Service] DbContext dbContext, 
        [Service] ITopicEventSender sender,
        Guid id)
    {
        var relation = await dbContext.Relations.SingleAsync(g => g.Id == id);
        dbContext.Relations.Remove(relation);

        await dbContext.SaveChangesAsync();

        await sender.SendAsync(nameof(Subscription.RelationRemoved), relation);
        
        return relation;
    }

}