namespace EtAlii.Adp.Client;

public partial class LeftToolbar
{
    private readonly ToolbarButton _addActionButton;
    private readonly ToolbarButton _addArtifactButton;
    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    private readonly ToolbarButton _groupItemsButton;
    private readonly ToolbarButton _ungroupItemsButton;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    private readonly ToolbarButton[] _leftToolbar;

    private void OnAddArtifactButtonClicked()
    {
        _addArtifactButton.IsToggled = true;
        _addActionButton.IsToggled = false;
    }
    private void OnAddActionButtonClicked()
    {
        _addArtifactButton.IsToggled = false;
        _addActionButton.IsToggled = true;
    }
    
    public LeftToolbar()
    {
        _addActionButton = new()
        {
            Clicked = OnAddActionButtonClicked,
            TooltipText = "Add Action",
            Icon = "e-critical-path",
        };
        _addArtifactButton = new()
        {
            Clicked = OnAddArtifactButtonClicked,
            TooltipText = "Add Artifact",
            Icon = "e-volume",
        };
        _groupItemsButton = new()
        {
            Clicked = () => { },
            TooltipText = "Group",
            Icon = "e-group-2",
        };
        _ungroupItemsButton = new()
        {
            Clicked = () => { },
            TooltipText = "Ungroup",
            Icon = "e-ungroup-2",
        };

        _leftToolbar =
        [
            _addActionButton,
            _addArtifactButton,
            _groupItemsButton,
            _ungroupItemsButton
        ];
    } 
}