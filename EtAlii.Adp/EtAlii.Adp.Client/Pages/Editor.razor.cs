using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client.Pages;

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

    protected override void OnInitialized()
    {
        InitDiagramModel();
    }

    private void OnClicked(ClickEventArgs e)
    {
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