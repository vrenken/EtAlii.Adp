using EtAlii.Adp.Client.Pages;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client.Controls;

public partial class RightToolbar
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
    
    [Parameter]
    public SfDiagramComponent Diagram { get; set; } = null!;

    [Parameter]
    public DiagramInteractions DiagramTool { get; set; } 
    
    public RightToolbar()
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
    
    private void ZoomChanged()
    {
        _resetButton.IsEnabled = false;
    }
    private void OnPanClick()
    {
        _panButton.IsToggled = true;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        _pointerButton.IsToggled = false;
        Diagram.InteractionController = DiagramInteractions.ZoomPan;
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
        Diagram.ResetZoom();
        _resetButton.IsEnabled = true;
    }

    private void UpdatePanAndPointerButtons()
    {
        if (Diagram.InteractionController == DiagramInteractions.Default)
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
        Diagram.FitToPage(new FitOptions { Mode = FitMode.Both, Region = DiagramRegion.Content });
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
        _pointerButton.IsToggled = Diagram.InteractionController == DiagramInteractions.Default;
        if (Diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = Diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            Diagram.BringIntoView(bound);
        }
        _resetButton.IsEnabled = false;
    }
    private void OnBringIntoCenterClick()
    {
        _pointerButton.IsToggled = Diagram.InteractionController == DiagramInteractions.Default;
        _resetButton.IsToggled = false;
        _panButton.IsToggled = false;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        _viewButton.IsToggled = false;
        _centerButton.IsToggled = false;
        _fitToPageButton.IsToggled = false;
        if (Diagram.SelectionSettings.Nodes.Count > 0)
        {
            var node = Diagram.SelectionSettings.Nodes[0];
            var bound = new DiagramRect(node.OffsetX - node.Width / 2, node.OffsetY - node.Height / 2, node.Width, node.Height);
            Diagram.BringIntoCenter(bound);
        }
        _resetButton.IsEnabled = false;
    }
    private void OnPointerClick()
    {
        Diagram.InteractionController = DiagramInteractions.SingleSelect | DiagramInteractions.MultipleSelect;
        _panButton.IsToggled = false;
        _zoomButtonIn.IsToggled = false;
        _zoomButtonOut.IsToggled = false;
        if (Diagram.SelectionSettings.Nodes.Count > 0)
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
        Diagram.Zoom(1.2, new DiagramPoint { X = 100, Y = 100 });
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
        Diagram.Zoom(1 / 1.2, new DiagramPoint { X = 100, Y = 100 });
    }    
}