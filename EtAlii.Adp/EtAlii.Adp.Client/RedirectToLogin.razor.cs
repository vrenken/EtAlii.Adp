namespace EtAlii.Adp.Client;

public partial class RedirectToLogin
{
    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}",
            forceLoad: true);
    }
}