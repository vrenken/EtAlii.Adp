using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client.Pages;

public partial class Editor
{
    private void InitDiagramModel()
    {
        var startItem = new Item { Id = Guid.NewGuid(), Position = new(300, 50), Size = new(145, 60), Label = "Start" };
        var node = NodeFactory.Create(startItem);
        _nodes.Add(node);

        var initItem = new Item { Id = Guid.NewGuid(), Position = new(300, 140), Size = new(145, 60), Label = "Init" };
        node = NodeFactory.Create(initItem);
        _nodes.Add(node);

        var conditionItem = new Item { Id = Guid.NewGuid(), Position = new(300, 230), Size = new(145, 60), Label = "Condition" };
        node = NodeFactory.Create(conditionItem);
        _nodes.Add(node);

        var printItem = new Item { Id = Guid.NewGuid(), Position = new(300, 320), Size = new(145, 60), Label = "Print" };
        node = NodeFactory.Create(printItem);
        _nodes.Add(node);

        var incrementItem = new Item { Id = Guid.NewGuid(), Position = new(300, 410), Size = new(145, 60), Label = "Increment" };
        node = NodeFactory.Create(incrementItem);
        _nodes.Add(node);

        var endItem = new Item { Id = Guid.NewGuid(), Position = new(300, 500), Size = new(145, 60), Label = "End" };
        node = NodeFactory.Create(endItem);
        _nodes.Add(node);

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
        CreateConnector(startItem, initItem);
        CreateConnector(initItem, conditionItem);
        CreateConnector(conditionItem, printItem);
        CreateConnector(conditionItem, endItem, "Yes", segment1, segment2);
        CreateConnector(printItem, incrementItem, "No");
        CreateConnector(incrementItem, conditionItem, null!, segment3, segment4);
    }

    // Method to create connector.
    private void CreateConnector(
        Item start, 
        Item end, string label = default!, 
        OrthogonalSegment segment1 = null!, 
        OrthogonalSegment segment2 = null!)
    {
        var diagramConnector = new Connector
        {
            // Represents the unique id of the connector.
            ID = $"connector{++_connectorCount}",
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
        _connectors.Add(diagramConnector);
    }
    
}