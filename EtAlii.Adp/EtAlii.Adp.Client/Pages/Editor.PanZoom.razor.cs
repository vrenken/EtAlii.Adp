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
        _panButtonItemCssClass = "tb-item-middle tb-item-selected";
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        _pointerButtonCssClass = "tb-item-start";
        _pointerButtonCssClass = "tb-item-start";
        _diagramTool = DiagramInteractions.ZoomPan;
        _view = true;
        _center = true;
        _reset = true;
    }

    private void OnResetClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-middle tb-item-selected";
            _panButtonItemCssClass = "tb-item-start";
        }
        else
        {
            _pointerButtonCssClass = "tb-item-start";
            _panButtonItemCssClass = "tb-item-middle tb-item-selected";
        }
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";       
        _diagram.ResetZoom();
        _reset = true;
    }

    private void OnFitToPageClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-middle tb-item-selected";
            _panButtonItemCssClass= "tb-item-start";
        }
        else
        {
            _pointerButtonCssClass = "tb-item-start";
            _panButtonItemCssClass = "tb-item-middle tb-item-selected";
        }
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        _diagram.FitToPage(new FitOptions { Mode = FitMode.Both, Region = DiagramRegion.Content });
        _reset = false;
    }
    private void OnBringIntoViewClick()
    {
        _panButtonItemCssClass = "tb-item-start";
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
        _pointerButtonCssClass = _diagramTool == DiagramInteractions.Default 
            ? "tb-item-middle tb-item-selected" 
            : "tb-item-start";
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect((node.OffsetX - (node.Width / 2)), node.OffsetY - (node.Height / 2), node.Width, node.Height);
            _diagram.BringIntoView(bound);
        }
        _reset = false;
    }
    private void OnBringIntoCenterClick()
    {
        _pointerButtonCssClass = _diagramTool == DiagramInteractions.Default 
            ? "tb-item-middle tb-item-selected" 
            : "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
        _panButtonItemCssClass = "tb-item-start";
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = _diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect((node.OffsetX - (node.Width / 2)), node.OffsetY - (node.Height / 2), node.Width, node.Height);
            _diagram.BringIntoCenter(bound);
        }
        _reset = false;
    }
    private void OnPointerClick()
    {
        _diagramTool = DiagramInteractions.SingleSelect | DiagramInteractions.MultipleSelect;
        _panButtonItemCssClass = "tb-item-start";
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        if (_diagram.SelectionSettings.Nodes.Count > 0)
        {
            _viewButtonCssClass = "tb-item-start";
            _centerButtonCssClass = "tb-item-start";
            _fitButtonCssClass = "tb-item-start";
            _view = false;
            _center = false;
        }
        _resetButtonCssClass = "tb-item-start";
        _pointerButtonCssClass = "tb-item-middle tb-item-selected";
        _reset = true;
    }

    private void OnZoomInItemClick()
    {
        if (_diagramTool == DiagramInteractions.Default)
        {
            _pointerButtonCssClass = "tb-item-middle tb-item-selected";
            _panButtonItemCssClass= "tb-item-start";
        }
        else
        {
            _pointerButtonCssClass = "tb-item-start";
            _panButtonItemCssClass = "tb-item-middle tb-item-selected";
        }
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
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
            _pointerButtonCssClass = "tb-item-middle tb-item-selected";
            _panButtonItemCssClass= "tb-item-start";
        }
        else
        {
            _pointerButtonCssClass = "tb-item-start";
            _panButtonItemCssClass = "tb-item-middle tb-item-selected";
        }
        _zoomButtonInItemCssClass = "tb-item-start";
        _zoomButtonOutItemCssClass = "tb-item-start";
        _viewButtonCssClass = "tb-item-start";
        _centerButtonCssClass = "tb-item-start";
        _fitButtonCssClass = "tb-item-start";
        _resetButtonCssClass = "tb-item-start";
        _diagram.Zoom(1 / 1.2, new DiagramPoint { X = 100, Y = 100 });
        _reset = false;
    }    
}