namespace EtAlii.Adp.Client;

public partial class RedirectToLogin
{
    protected override void OnInitialized()
    {
        var url = $"Account/Login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}";
        NavigationManager.NavigateTo(url, forceLoad: true);
    }
}