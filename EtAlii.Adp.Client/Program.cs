
using System.Reflection;
using EtAlii.Adp;

SyncfusionLicenseProvider.RegisterLicense("MzE5MzAwOEAzMjM1MmUzMDJlMzBvWjc1OHlQWjNkYXB4cm9BUlFSL013SkZwMkE4SGx5ZXFVSVhpSGN2eTRzPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var configuration = builder.Configuration.GetSection("Seq");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var servicePort = 7276;
var baseAddressBuilder = new UriBuilder(builder.HostEnvironment.BaseAddress);
var clientBaseAddress = baseAddressBuilder.Uri;
baseAddressBuilder.Path = "/api";
baseAddressBuilder.Port = servicePort;
var serviceHttpBaseAddress = baseAddressBuilder.Uri;
baseAddressBuilder.Scheme = "wss";
var webSocketHttpBaseAddress = baseAddressBuilder.Uri;


builder.Services
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddApplicationScopeLogger(logger =>
            {
                logger.Scope["ApplicationName"] = Path.GetFileName(Assembly.GetExecutingAssembly().GetName().Name!);
                logger.Scope["ApplicationRun"] = Guid.NewGuid().ToString("N").ToUpper();
                logger.Scope["MachineName"] = Environment.MachineName;
            },
            logging =>
            {
                logging.AddSeq(configuration);
            });
    })
    .AddScoped(_ => new HttpClient { BaseAddress = clientBaseAddress})
    .AddSyncfusionBlazor()
    .AddSingleton<EditPageViewModel>()
    .AddAdpClient(profile: AdpClientProfileKind.Http)
    .ConfigureHttpClient(client => client.BaseAddress = serviceHttpBaseAddress)
    .ConfigureWebSocketClient(client => client.Uri = webSocketHttpBaseAddress);
    

var host = builder.Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
logger.LogInformation("Starting client");
    
await host.RunAsync();