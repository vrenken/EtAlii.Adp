using EtAlii.Adp.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("MzE5MzAwOEAzMjM1MmUzMDJlMzBvWjc1OHlQWjNkYXB4cm9BUlFSL013SkZwMkE4SGx5ZXFVSVhpSGN2eTRzPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
