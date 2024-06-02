using Microsoft.AspNetCore.Components;

namespace BlazorApp2;

public partial class PersonalData
{
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _ = await UserAccessor.GetRequiredUserAsync(HttpContext);
    }
}