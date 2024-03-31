using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.Navigations;

namespace EtAlii.Adp.Client.Pages;

public partial class Editor
{
    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _zoomInButton = null!;
    private string _zoomButtonOutItemCssClass = null!;

    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _zoomOutButton = null!;
    private string _zoomButtonInItemCssClass = null!;

    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _panButton = null!;
    private string _panButtonItemCssClass = null!;

    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _pointerButton = null!;
    private string _pointerButtonCssClass = null!;
    
    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _viewButton = null!;
    private string _viewButtonCssClass = null!;
    private bool _view;

    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _centerButton = null!;
    private string _centerButtonCssClass = null!;
    private bool _center;

    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _fitToPageButton = null!;
    private string _fitButtonCssClass = null!;
    
    // ReSharper disable once NotAccessedField.Local
    private ToolbarItem _resetButton = null!;
    private string _resetButtonCssClass = null!;
    private bool _reset;
    
    private DiagramInteractions _diagramTool = DiagramInteractions.ZoomPan;
    
    private void OnPanClick()
    {
        _panButtonItemCssClass = "tb-item-selected";
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        _pointerButtonCssClass = string.Empty;
        _pointerButtonCssClass = string.Empty;
        _diagramTool = DiagramInteractions.ZoomPan;
        _view = true;
        _center = true;
        _reset = true;
    }
    
    private void OnResetClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-selected";
            _panButtonItemCssClass = string.Empty;
        }
        else
        {
            _pointerButtonCssClass = string.Empty;
            _panButtonItemCssClass = "tb-item-selected";
        }
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _resetButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;       
        _diagram.ResetZoom();
        _reset = true;
    }

    private void OnFitToPageClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-selected";
            _panButtonItemCssClass= string.Empty;
        }
        else
        {
            _pointerButtonCssClass = string.Empty;
            _panButtonItemCssClass = "tb-item-selected";
        }
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _resetButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        _diagram.FitToPage(new FitOptions { Mode = FitMode.Both, Region = DiagramRegion.Content });
        _reset = false;
    }
    private void OnBringIntoViewClick()
    {
        _panButtonItemCssClass = string.Empty;
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        _resetButtonCssClass = string.Empty;
        _pointerButtonCssClass = _diagramTool == DiagramInteractions.Default 
            ? "tb-item-selected" 
            : string.Empty;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            _diagram.BringIntoView(bound);
        }
        _reset = false;
    }
    private void OnBringIntoCenterClick()
    {
        _pointerButtonCssClass = _diagramTool == DiagramInteractions.Default 
            ? "tb-item-selected" 
            : string.Empty;
        _resetButtonCssClass = string.Empty;
        _panButtonItemCssClass = string.Empty;
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            _diagram.BringIntoCenter(bound);
        }
        _reset = false;
    }
    private void OnPointerClick()
    {
        _diagramTool = DiagramInteractions.SingleSelect | DiagramInteractions.MultipleSelect;
        _panButtonItemCssClass = string.Empty;
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            _viewButtonCssClass = string.Empty;
            _centerButtonCssClass = string.Empty;
            _fitButtonCssClass = string.Empty;
            _view = false;
            _center = false;
        }
        _resetButtonCssClass = string.Empty;
        _pointerButtonCssClass = "tb-item-selected";
        _reset = true;
    }

    private void OnZoomInItemClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-selected";
            _panButtonItemCssClass= string.Empty;
        }
        else
        {
            _pointerButtonCssClass = string.Empty;
            _panButtonItemCssClass = "tb-item-selected";
        }
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        _resetButtonCssClass = string.Empty;
        _diagram.Zoom(1.2, new DiagramPoint { X = 100, Y = 100 });
        _reset = false;
    }

    private void ZoomChanged()
    {
        _reset = false;
    }

    private void OnZoomOutItemClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-selected";
            _panButtonItemCssClass= string.Empty;
        }
        else
        {
            _pointerButtonCssClass = string.Empty;
            _panButtonItemCssClass = "tb-item-selected";
        }
        _zoomButtonInItemCssClass = string.Empty;
        _zoomButtonOutItemCssClass = string.Empty;
        _viewButtonCssClass = string.Empty;
        _centerButtonCssClass = string.Empty;
        _fitButtonCssClass = string.Empty;
        _resetButtonCssClass = string.Empty;
        _diagram.Zoom(1 / 1.2, new DiagramPoint { X = 100, Y = 100 });
        _reset = false;
    }    
}