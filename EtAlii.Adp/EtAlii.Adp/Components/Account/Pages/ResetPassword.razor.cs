﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace EtAlii.Adp.Components.Account.Pages;

public partial class ResetPassword
{
    private IEnumerable<IdentityError>? _identityErrors;

    [SupplyParameterFromForm] private InputModel Input { get; set; } = new();

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    [SupplyParameterFromQuery] private string? Code { get; set; }

    private string? Message => _identityErrors is null
        ? null
        : $"Error: {string.Join(", ", _identityErrors.Select(error => error.Description))}";

    protected override void OnInitialized()
    {
        if (Code is null)
        {
            RedirectManager.RedirectTo("Account/InvalidPasswordReset");
        }

        Input.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
    }

    private async Task OnValidSubmitAsync()
    {
        var user = await UserManager.FindByEmailAsync(Input.Email);
        if (user is null)
        {
            // Don't reveal that the user does not exist
            RedirectManager.RedirectTo("Account/ResetPasswordConfirmation");
        }

        var result = await UserManager.ResetPasswordAsync(user, Input.Code, Input.Password);
        if (result.Succeeded)
        {
            RedirectManager.RedirectTo("Account/ResetPasswordConfirmation");
        }

        _identityErrors = result.Errors;
    }

    private sealed class InputModel
    {
        [Required] [EmailAddress] public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";

        [Required] public string Code { get; set; } = "";
    }
}