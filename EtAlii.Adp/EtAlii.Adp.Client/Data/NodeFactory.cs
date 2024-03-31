using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public static class NodeFactory
{
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
            // Shape = new FlowShape { Type = NodeShapes.Flow, Shape = shape },
            // Defines the annotation collection of the node.
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
            Data = item
        };
        return node;
    }
}
