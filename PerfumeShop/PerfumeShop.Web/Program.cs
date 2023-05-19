var builder = WebApplication.CreateBuilder(args);
var logger = WebDependencies.SetLogger(builder.Configuration, builder.Logging);

WebDependencies.SetServices(builder.Configuration, builder.Services);

var app = builder.Build();
await WebDependencies.ApplyInitializeDbAsync(app.Services, logger);

WebDependencies.SetMiddleware(app);

logger.Information("Application started");

app.Run();