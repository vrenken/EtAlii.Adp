using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace BlazorApp2;

public partial class ExternalLoginPicker
{
    private AuthenticationScheme[] _externalLogins = [];
    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _externalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToArray();
    }
}