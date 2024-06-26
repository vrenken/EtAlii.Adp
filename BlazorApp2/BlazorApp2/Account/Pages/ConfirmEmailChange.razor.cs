﻿using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorApp2;

public partial class ConfirmEmailChange
{
    private string? _message;
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? UserId { get; set; }
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? Email { get; set; }
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? Code { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserId is null || Email is null || Code is null)
        {
            RedirectManager.RedirectToWithStatus(
                "Account/Login", "Error: Invalid email change confirmation link.", HttpContext);
        }

        var user = await UserManager.FindByIdAsync(UserId);
        if (user is null)
        {
            _message = "Unable to find user with Id '{userId}'";
            return;
        }

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
        var result = await UserManager.ChangeEmailAsync(user, Email, code);
        if (!result.Succeeded)
        {
            _message = "Error changing email.";
            return;
        }

        // In our UI email and user name are one and the same, so when we update the email
        // we need to update the user name.
        var setUserNameResult = await UserManager.SetUserNameAsync(user, Email);
        if (!setUserNameResult.Succeeded)
        {
            _message = "Error changing user name.";
            return;
        }

        await SignInManager.RefreshSignInAsync(user);
        _message = "Thank you for confirming your email change.";
    }
}