using System.Diagnostics;
using EtAlii.Adp.Service;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;
using DbContext = EtAlii.Adp.Service.DbContext;

var specialFolder = "Data";
var databaseFile = Path.Combine(specialFolder, "Database.db");  
Directory.CreateDirectory(specialFolder);

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<DbContext>(options => options.UseSqlite($"Data Source='{databaseFile}'"))
    .AddCors();

var apiBuilder = builder.Services
    .AddGraphQLServer()
    .InitializeOnStartup()
    .AddMutationConventions()
    .AddInMemorySubscriptions()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .RegisterDbContext<DbContext>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

if(Debugger.IsAttached) // or builder.Environment.IsDevelopment()
{
    apiBuilder
        .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
        .ModifyRequestOptions(options => options.IncludeExceptionDetails = true);
}


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true) // allow any origin  
    .AllowCredentials());               // allow credentials 

app.MapGet("/", () => "Hello World!");

app
    .MapGraphQL("/api")
    .WithOptions(new GraphQLServerOptions { Tool = { Enable = Debugger.IsAttached } }); // or app.Environment.IsDevelopment()

// Let's setup the database.
using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
context.Database.EnsureCreated();

app.Run();