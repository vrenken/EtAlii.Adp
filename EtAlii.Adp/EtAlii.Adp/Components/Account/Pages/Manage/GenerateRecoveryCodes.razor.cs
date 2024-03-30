﻿using EtAlii.Adp.Data;
using Microsoft.AspNetCore.Components;

namespace EtAlii.Adp.Components.Account.Pages.Manage;

public partial class GenerateRecoveryCodes
{
    private string? _message;
    private ApplicationUser _user = default!;
    private IEnumerable<string>? _recoveryCodes;

    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _user = await UserAccessor.GetRequiredUserAsync(HttpContext);

        var isTwoFactorEnabled = await UserManager.GetTwoFactorEnabledAsync(_user);
        if (!isTwoFactorEnabled)
        {
            throw new InvalidOperationException(
                "Cannot generate recovery codes for user because they do not have 2FA enabled.");
        }
    }

    private async Task OnSubmitAsync()
    {
        var userId = await UserManager.GetUserIdAsync(_user);
        _recoveryCodes = await UserManager.GenerateNewTwoFactorRecoveryCodesAsync(_user, 10);
        _message = "You have generated new recovery codes.";

        Logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes", userId);
    }
}