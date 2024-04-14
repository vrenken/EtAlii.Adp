namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Subscription 
{
    [Subscribe]
    public Graph GraphAdded([EventMessage] Graph graph) => graph;
    
    [Subscribe]
    public Graph GraphChanged([EventMessage] Graph graph) => graph;
    
    [Subscribe]
    public Graph GraphRemoved([EventMessage] Graph graph) => graph;

}