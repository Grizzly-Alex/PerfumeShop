var builder = WebApplication.CreateBuilder(args);
var logger = WebDependencies.SetLogger(builder.Configuration, builder.Logging);

DbConfiguration.SetDbContext(builder.Configuration, builder.Services);
WebDependencies.SetServices(builder.Services);

var app = builder.Build();
await WebDependencies.ApplyInitializeDbAsync(app.Services, logger);

WebDependencies.SetMiddleware(app);

logger.Information("Application started");

app.Run();