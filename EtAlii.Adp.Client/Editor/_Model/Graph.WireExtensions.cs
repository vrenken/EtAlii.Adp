namespace EtAlii.Adp.Client;

public static class GraphWireExtensions
{
    public static Graph ToLocal(this IAddGraph_AddGraph_Graph graph)
    {
        return new Graph
        {
            Id = Guid.Parse(graph.Id),
            Name = graph.Name,
        };
    }
}