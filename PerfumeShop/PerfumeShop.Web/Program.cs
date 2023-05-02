var builder = WebApplication.CreateBuilder(args);
var logger = WebDependencies.SetLogger(builder.Configuration, builder.Logging);

DbConfiguration.SetDbContext(builder.Configuration, builder.Services);
WebDependencies.SetServices(builder.Configuration, builder.Services);

var app = builder.Build();
await WebDependencies.ApplyInitializeDbAsync(app.Services, logger);

WebDependencies.SetMiddleware(app, builder.Configuration);

logger.Information("Application started");

app.Run();