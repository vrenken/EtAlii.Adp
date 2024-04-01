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
    
    private DiagramInteractions _diagramTool = DiagramInteractions.ZoomPan;

    public Editor()
    {
        _zoomButtonIn = new()
        {
            Clicked = OnZoomInItemClick,
            TooltipText = "Zoom in",
            Icon = "e-zoom-in",
        };

        _zoomButtonOut = new()
        {
            Clicked = OnZoomOutItemClick,
            TooltipText = "Zoom out",
            Icon = "e-zoom-out"
        };
        _resetButton = new()
        {
            Clicked = OnResetClick,
            TooltipText = "Reset",
            Icon = "e-refresh",
        };
        _panButton = new()
        {
            Clicked = OnPanClick,
            TooltipText = "Pan",
            Icon = "e-pan",
        };
        _pointerButton = new()
        {
            Clicked = OnPointerClick,
            TooltipText = "Select",
            Icon = "e-mouse-pointer"
        };
        _viewButton = new()
        {
            Clicked = OnBringIntoViewClick,
            TooltipText = "Bring into view",
            Icon = "e-bring-to-view" 
        };
        _centerButton = new()
        {
            Clicked = OnBringIntoCenterClick,
            TooltipText = "Bring into center",
            Icon = "e-bring-to-center"
        };
        _fitToPageButton = new()
        {
            Clicked = OnFitToPageClick,
            TooltipText = "Fit to page",
            Icon = "e-zoom-to-fit"

        };

        _rightToolbar =
        [
            _zoomButtonOut,
            _zoomButtonIn,
            _panButton,
            _pointerButton,
            _viewButton,
            _centerButton,
            _fitToPageButton,
            _resetButton
        ];
    }
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