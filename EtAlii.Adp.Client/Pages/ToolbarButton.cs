namespace EtAlii.Adp.Client.Pages;

public class ToolbarButton
{
    public static readonly ToolbarButton Separatator = new();
    
    public Action Clicked = () => { };
    
    public string Icon = string.Empty;
    public string TooltipText = string.Empty;
    public string CssClass => IsToggled ? "tb-item-selected" : string.Empty;
    public bool IsToggled;
    public bool IsEnabled = true;
    public bool IsDisabled => !IsEnabled;
}