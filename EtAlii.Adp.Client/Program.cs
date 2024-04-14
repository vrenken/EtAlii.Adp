using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EtAlii.Adp.Client;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("MzE5MzAwOEAzMjM1MmUzMDJlMzBvWjc1OHlQWjNkYXB4cm9BUlFSL013SkZwMkE4SGx5ZXFVSVhpSGN2eTRzPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var servicePort = 7276;
var baseAddressBuilder = new UriBuilder(builder.HostEnvironment.BaseAddress);
//var clientBaseAddress = baseAddressBuilder.Uri;
baseAddressBuilder.Path = "/api";
baseAddressBuilder.Port = servicePort;
var serviceHttpBaseAddress = baseAddressBuilder.Uri;
baseAddressBuilder.Scheme = "wss";
var webSocketHttpBaseAddress = baseAddressBuilder.Uri;

builder.Services
    //.AddScoped(_ => new HttpClient { BaseAddress = clientBaseAddress})
    .AddSyncfusionBlazor()
    .AddSingleton<EditPageViewModel>()
    .AddAdpClient()
    .ConfigureHttpClient(config => config.BaseAddress = serviceHttpBaseAddress)
    .ConfigureWebSocketClient(config => config.Uri = webSocketHttpBaseAddress);

await builder
    .Build()
    .RunAsync();