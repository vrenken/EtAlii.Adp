using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client.Pages;

public partial class Editor
{
    private readonly ToolbarButton _zoomButtonOut;
    private readonly ToolbarButton _zoomButtonIn;
    private readonly ToolbarButton _panButton;
    private readonly ToolbarButton _pointerButton;
    private readonly ToolbarButton _viewButton;
    private readonly ToolbarButton _centerButton;
    private readonly ToolbarButton _fitToPageButton;
    private readonly ToolbarButton _resetButton;

    private readonly ToolbarButton[] _rightToolbar;
    
    private void OnPanClick()
    {
        _panButton.IsToggled = true;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _pointerButton.IsToggled = false;
        _diagramTool = DiagramInteractions.ZoomPan;
        _viewButton.IsEnabled = true;
        _centerButton.IsEnabled = true;
        _resetButton.IsEnabled = true;
    }
    
    private void OnResetClick()
    {
        UpdatePanAndPointerButtons();
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;       
        _resetButton.IsToggled = false;
        _diagram.ResetZoom();
        _resetButton.IsEnabled = true;
    }

    private void UpdatePanAndPointerButtons()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButton.IsToggled = true;
            _panButton.IsToggled = false;
        }
        else
        {
            _pointerButton.IsToggled = false;
            _panButton.IsToggled = true;
        }
    }
    
    private void OnFitToPageClick()
    {
        UpdatePanAndPointerButtons();
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _resetButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _diagram.FitToPage(new FitOptions { Mode = FitMode.Both, Region = DiagramRegion.Content });
        _resetButton.IsEnabled = false;
    }
    private void OnBringIntoViewClick()
    {
        _panButton.IsToggled = false;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _resetButton.IsToggled = false;
        _pointerButton.IsToggled = _diagramTool == DiagramInteractions.Default;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            _diagram.BringIntoView(bound);
        }
        _resetButton.IsEnabled = false;
    }
    private void OnBringIntoCenterClick()
    {
        _pointerButton.IsToggled = _diagramTool == DiagramInteractions.Default;
        _resetButton.IsToggled = false;
        _panButton.IsToggled = false;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            _diagram.BringIntoCenter(bound);
        }
        _resetButton.IsEnabled = false;
    }
    private void OnPointerClick()
    {
        _diagramTool = DiagramInteractions.SingleSelect | DiagramInteractions.MultipleSelect;
        _panButton.IsToggled = false;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            _viewButton.IsToggled = false;
            _centerButton.IsToggled = false;
            _fitToPageButton.IsToggled = false;
            _viewButton.IsEnabled = false;
            _centerButton.IsEnabled = false;
        }
        _resetButton.IsToggled = false;
        _pointerButton.IsToggled = true;
        _resetButton.IsEnabled = true;
    }

    private void OnZoomInItemClick()
    {
        UpdatePanAndPointerButtons();
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _resetButton.IsToggled = false;
        _diagram.Zoom(1.2, new DiagramPoint { X = 100, Y = 100 });
    }

    private void ZoomChanged()
    {
        _resetButton.IsEnabled = false;
    }

    private void OnZoomOutItemClick()
    {
        UpdatePanAndPointerButtons();
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _resetButton.IsToggled = false;
        _diagram.Zoom(1 / 1.2, new DiagramPoint { X = 100, Y = 100 });
    }    
}