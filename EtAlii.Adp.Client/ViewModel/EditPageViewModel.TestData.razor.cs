using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public partial class EditPageViewModel
{
    private (IReadOnlyList<Node>, IReadOnlyList<Connector>) InitDiagramModel()
    {
        var nodes = new List<Node>();
        var connectors = new List<Connector>();
        
        var startItem = new Item { Id = Guid.NewGuid(), Position = new(300, 50), Size = new(145, 60), Label = "Start" };
        var node = NodeFactory.Create(startItem);
        nodes.Add(node);

        var initItem = new Item { Id = Guid.NewGuid(), Position = new(300, 140), Size = new(145, 60), Label = "Init" };
        node = NodeFactory.Create(initItem);
        nodes.Add(node);

        var conditionItem = new Item { Id = Guid.NewGuid(), Position = new(300, 230), Size = new(145, 60), Label = "Condition" };
        node = NodeFactory.Create(conditionItem);
        nodes.Add(node);

        var printItem = new Item { Id = Guid.NewGuid(), Position = new(300, 320), Size = new(145, 60), Label = "Print" };
        node = NodeFactory.Create(printItem);
        nodes.Add(node);

        var incrementItem = new Item { Id = Guid.NewGuid(), Position = new(300, 410), Size = new(145, 60), Label = "Increment" };
        node = NodeFactory.Create(incrementItem);
        nodes.Add(node);

        var endItem = new Item { Id = Guid.NewGuid(), Position = new(300, 500), Size = new(145, 60), Label = "End" };
        node = NodeFactory.Create(endItem);
        nodes.Add(node);

        // Creates orthogonal connector.
        var segment1 = new OrthogonalSegment
        {
            Type = ConnectorSegmentType.Orthogonal,
            Direction = Direction.Right,
            Length = 30,
        };
        var segment2 = new OrthogonalSegment
        {
            Type = ConnectorSegmentType.Orthogonal,
            Length = 300,
            Direction = Direction.Bottom,
        };
        var segment3 = new OrthogonalSegment
        {
            Type = ConnectorSegmentType.Orthogonal,
            Length = 30,
            Direction = Direction.Left,
        };
        var segment4 = new OrthogonalSegment
        {
            Type = ConnectorSegmentType.Orthogonal,
            Length = 200,
            Direction = Direction.Top,
        };
        CreateConnector(connectors, startItem, initItem);
        CreateConnector(connectors, initItem, conditionItem);
        CreateConnector(connectors, conditionItem, printItem);
        CreateConnector(connectors, conditionItem, endItem, "Yes", segment1, segment2);
        CreateConnector(connectors, printItem, incrementItem, "No");
        CreateConnector(connectors, incrementItem, conditionItem, null!, segment3, segment4);

        return (nodes, connectors);
    }

    // Method to create connector.
    private void CreateConnector(
        List<Connector> connectors,
        Item start, 
        Item end, string label = default!, 
        OrthogonalSegment segment1 = null!, 
        OrthogonalSegment segment2 = null!)
    {
        var diagramConnector = new Connector
        {
            // Represents the unique id of the connector.
            ID = $"connector{connectors.Count}",
            SourceID = start.Id.ToString(),
            TargetID = end.Id.ToString(),
        };
        if (label != default!)
        {
            // Represents the annotation of the connector.
            var annotation = new PathAnnotation
            {
                Content = label,
                Style = new TextStyle { Fill = "white" }
            };
            diagramConnector.Annotations = [annotation];
        }
        if (segment1 != null!)
        {
            // Represents the segment type of the connector.
            diagramConnector.Type = ConnectorSegmentType.Orthogonal;
            diagramConnector.Segments = [segment1, segment2];
        }
        connectors.Add(diagramConnector);
    }
    
}