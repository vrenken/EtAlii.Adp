namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Subscription 
{
    private readonly ILogger<Subscription> _logger;

    public Subscription(ILogger<Subscription> logger)
    {
        _logger = logger;
    }

    [Subscribe]
    public Item ItemAdded([EventMessage] Item item)
    {
        _logger.LogInformation("GraphQL {SubscriptionName} subscription called", nameof(ItemAdded));

        return item;
    }

    [Subscribe]
    public Item ItemChanged([EventMessage] Item item)
    {
        _logger.LogInformation("GraphQL {SubscriptionName} subscription called", nameof(ItemChanged));

        return item;
    }

    [Subscribe]
    public Item ItemRemoved([EventMessage] Item item)
    {
        _logger.LogInformation("GraphQL {SubscriptionName} subscription called", nameof(ItemRemoved));

        return item;
    }
}