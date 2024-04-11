﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace EtAlii.Adp.Components.Account.Shared;

public partial class ExternalLoginPicker
{
    private AuthenticationScheme[] _externalLogins = [];

    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _externalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToArray();
    }
}