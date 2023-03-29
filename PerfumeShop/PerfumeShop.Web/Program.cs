var builder = WebApplication.CreateBuilder(args);

DbConfiguration.SetDbContext(builder.Configuration, builder.Services);
WebDependencies.SetServices(builder.Services);

var app = builder.Build();

WebDependencies.SetMiddleware(app);

app.Run();