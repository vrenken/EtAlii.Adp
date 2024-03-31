namespace EtAlii.Adp.Client.Pages;

public partial class Editor
{
    private string _addArtifactButtonCssClass = null!;
    private string _addActionButtonCssClass = null!;

    private void OnAddArtifactButtonClicked()
    {
        _addArtifactButtonCssClass = "tb-item-middle tb-item-selected";
        _addActionButtonCssClass= "tb-item-start";
    }
    private void OnAddActionButtonClicked()
    {
        _addActionButtonCssClass = "tb-item-middle tb-item-selected";
        _addArtifactButtonCssClass= "tb-item-start";
    }
}