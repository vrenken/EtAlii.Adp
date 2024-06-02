﻿using BlazorApp2.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp2;

public partial class ExternalLogins
{
    public const string LinkLoginCallbackAction = "LinkLoginCallback";
    private ApplicationUser _user = default!;
    private IList<UserLoginInfo>? _currentLogins;
    private IList<AuthenticationScheme>? _otherLogins;
    private bool _showRemoveButton;
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;
    [SupplyParameterFromForm] private string? LoginProvider { get; set; }
    [SupplyParameterFromForm] private string? ProviderKey { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? Action { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _user = await UserAccessor.GetRequiredUserAsync(HttpContext);
        _currentLogins = await UserManager.GetLoginsAsync(_user);
        _otherLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync())
            .Where(auth => _currentLogins.All(ul => auth.Name != ul.LoginProvider))
            .ToList();

        string? passwordHash = null;
        if (UserStore is IUserPasswordStore<ApplicationUser> userPasswordStore)
        {
            passwordHash = await userPasswordStore.GetPasswordHashAsync(_user, HttpContext.RequestAborted);
        }

        _showRemoveButton = passwordHash is not null || _currentLogins.Count > 1;

        if (HttpMethods.IsGet(HttpContext.Request.Method) && Action == LinkLoginCallbackAction)
        {
            await OnGetLinkLoginCallbackAsync();
        }
    }

    private async Task OnSubmitAsync()
    {
        var result = await UserManager.RemoveLoginAsync(_user, LoginProvider!, ProviderKey!);
        if (!result.Succeeded)
        {
            RedirectManager.RedirectToCurrentPageWithStatus("Error: The external login was not removed.", HttpContext);
        }

        await SignInManager.RefreshSignInAsync(_user);
        RedirectManager.RedirectToCurrentPageWithStatus("The external login was removed.", HttpContext);
    }

    private async Task OnGetLinkLoginCallbackAsync()
    {
        var userId = await UserManager.GetUserIdAsync(_user);
        var info = await SignInManager.GetExternalLoginInfoAsync(userId);
        if (info is null)
        {
            RedirectManager.RedirectToCurrentPageWithStatus("Error: Could not load external login info.", HttpContext);
        }

        var result = await UserManager.AddLoginAsync(_user, info);
        if (!result.Succeeded)
        {
            RedirectManager.RedirectToCurrentPageWithStatus(
                "Error: The external login was not added. External logins can only be associated with one account.",
                HttpContext);
        }

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        RedirectManager.RedirectToCurrentPageWithStatus("The external login was added.", HttpContext);
    }
}