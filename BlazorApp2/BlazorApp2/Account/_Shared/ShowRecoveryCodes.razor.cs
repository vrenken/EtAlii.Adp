﻿using Microsoft.AspNetCore.Components;

namespace BlazorApp2;

public partial class ShowRecoveryCodes
{
    [Parameter] public string[] RecoveryCodes { get; set; } = [];
    [Parameter] public string? StatusMessage { get; set; }
}