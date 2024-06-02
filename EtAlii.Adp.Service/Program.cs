using System.Diagnostics;
using System.Reflection;
using EtAlii.Adp;
using EtAlii.Adp.Service;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;
using DbContext = EtAlii.Adp.Service.DbContext;
using Subscription = EtAlii.Adp.Service.Subscription;

var specialFolder = "Data";
var databaseFile = Path.Combine(specialFolder, "Database.db");  
Directory.CreateDirectory(specialFolder);

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection("Seq");

builder.Services
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.AddApplicationScopeLogger(logger =>
        {
            logger.Scope["ApplicationName"] = Path.GetFileName(Assembly.GetExecutingAssembly().GetName().Name!);
            logger.Scope["ApplicationRun"] = Guid.NewGuid().ToString("N").ToUpper();
            logger.Scope["EnvironmentName"] = builder.Environment.EnvironmentName;
            logger.Scope["MachineName"] = Environment.MachineName;
        },
        logging =>
        {
            logging.AddSeq(configuration);
        });
    })
    .AddDbContext<DbContext>(options => options.UseSqlite($"Data Source='{databaseFile}'"))
    .AddCors();

var apiBuilder = builder.Services
    .AddGraphQLServer()
    .InitializeOnStartup()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .RegisterDbContext<DbContext>()
    .AddInMemorySubscriptions()
    .AddMutationConventions()
    .AddSorting()
    .AddFiltering()
    .AddProjections();

if(Debugger.IsAttached) // or builder.Environment.IsDevelopment()
{
    apiBuilder
        .AddSubscriptionDiagnostics()
        .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
        .ModifyRequestOptions(options => options.IncludeExceptionDetails = true);
}

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    //.AllowAnyOrigin()
    .SetIsOriginAllowed(_ => true) // allow any origin  
    .AllowCredentials());               // allow credentials 

app.UseHttpsRedirection();

app.MapGet("/", () => "ADP is running");

// var webSocketOptions = new WebSocketOptions();
// webSocketOptions.AllowedOrigins.Add("https://localhost:7233");
// webSocketOptions.AllowedOrigins.Add("https://localhost:7276");

app
    .UseWebSockets();//webSocketOptions)
//    .UseRouting();

app
    .MapGraphQL("/api")
    .WithOptions(new GraphQLServerOptions
    {
        Tool = { Enable = Debugger.IsAttached },
    }); // or app.Environment.IsDevelopment()

// Let's setup the database.
using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
context.Database.EnsureCreated();

app.Run();