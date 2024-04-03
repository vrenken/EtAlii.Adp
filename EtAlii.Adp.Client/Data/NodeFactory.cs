using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public static class NodeFactory
{
    private static readonly NodeConstraints NodeConstraints =
        NodeConstraints.Resize |
        NodeConstraints.RoutingObstacle |
        NodeConstraints.OutConnect |
        NodeConstraints.InConnect |
        NodeConstraints.Delete |
        NodeConstraints.PointerEvents |
        //NodeConstraints.Rotate |
        NodeConstraints.Drag |
        NodeConstraints.Select |
        NodeConstraints.HideThumbs;

    public static Node Create(Item item)
    {
        var node = new Node
        {
            //Represents the unique id of the node.
            ID = item.Id.ToString(),
            // Defines the position of the node.
            OffsetX = item.Position.X,
            OffsetY = item.Position.Y,
            // Defines the size of the node.
            Width = item.Size.X,
            Height = item.Size.Y,
            // Defines the style of the node.
            Style = new ShapeStyle { Fill = "#357BD2", StrokeColor = "White" },
            // Defines the shape of the node.
            Shape = new BasicShape
            {
                Type = NodeShapes.Basic,
                Shape = NodeBasicShapes.Rectangle,
                //Sets the corner radius to the node shape.
                CornerRadius = 10
            },
            Constraints = NodeConstraints, 

            //Constraints = nodecon
            Annotations =
            [
                new ShapeAnnotation
                {
                    Content = item.Label,
                    Style = new TextStyle
                    {
                        Color = "White",
                        Fill = "transparent"
                    }
                }
            ],
            Ports = 
            [
                CreatePort(1.0f, 0.5f),
                CreatePort(0.0f, 0.5f)
            ],
            Data = item
        };
        return node;
    }

    private static PointPort CreatePort(float x, float y)
    {
        return new PointPort
        {
            Style = new ShapeStyle { Fill = "white" },
            Offset = new DiagramPoint { X = x, Y = y },
            Visibility = PortVisibility.Visible,
            Width = 8, Height = 8,
            Shape = PortShapes.Circle
        };
    }
}
