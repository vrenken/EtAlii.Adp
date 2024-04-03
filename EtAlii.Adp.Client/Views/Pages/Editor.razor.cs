using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public partial class Editor
{
    private SfDiagramComponent _diagram = null!;
    private int _connectorCount;

    /// <summary>
    /// Defines Diagram's nodes collection.
    /// </summary>
    private readonly DiagramObjectCollection<Node> _nodes = new();
    /// <summary>
    /// Defines Diagram's connectors collection.
    /// </summary>
    private readonly DiagramObjectCollection<Connector> _connectors = new();
    
    public Editor()
    {
    }
    protected override void OnInitialized()
    {
        InitDiagramModel();
    }

    private void OnClicked(ClickEventArgs e)
    {
        // if (_addActionButton.IsToggled)
        // {
        //     var startItem = new Item { Id = Guid.NewGuid(), Position = new((float)e.Position.X, (float)e.Position.Y), Size = new(145, 60), Label = "New action" };
        //     var node = NodeFactory.Create(startItem);
        //     _nodes.Add(node);
        // }
        
        
        // if (e.Element == null)
        // {
        //     _diagramTool = DiagramInteractions.ZoomPan;
        //     //StateHasChanged();
        // }
        // else if (e.Element != null)
        // {
        //     _diagramTool = DiagramInteractions.Default;
        //     //StateHasChanged();
        // }
    }
}