namespace EtAlii.Adp.Client.Pages;

public partial class Editor
{
    private string _addArtifactButtonCssClass = null!;
    private string _addActionButtonCssClass = null!;

    private void OnAddArtifactButtonClicked()
    {
        _addArtifactButtonCssClass = "tb-item-selected";
        _addActionButtonCssClass= string.Empty;
    }
    private void OnAddActionButtonClicked()
    {
        _addActionButtonCssClass = "tb-item-selected";
        _addArtifactButtonCssClass= string.Empty;
    }
}