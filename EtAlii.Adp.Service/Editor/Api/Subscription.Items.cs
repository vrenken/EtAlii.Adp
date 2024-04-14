namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Subscription 
{
    [Subscribe]
    public Item ItemAdded([EventMessage] Item item) => item;
    
    [Subscribe]
    public Item ItemChanged([EventMessage] Item item) => item;
    
    [Subscribe]
    public Item ItemRemoved([EventMessage] Item item) => item;

}