﻿using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorApp2.Components.Account.Pages;

public partial class ConfirmEmail
{
    private string? _statusMessage;
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? UserId { get; set; }
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? Code { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserId is null || Code is null)
        {
            RedirectManager.RedirectTo("");
        }

        var user = await UserManager.FindByIdAsync(UserId);
        if (user is null)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            _statusMessage = $"Error loading user with ID {UserId}";
        }
        else
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
            var result = await UserManager.ConfirmEmailAsync(user, code);
            _statusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
        }
    }
}