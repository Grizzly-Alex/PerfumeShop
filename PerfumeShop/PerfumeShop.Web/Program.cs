using PerfumeShop.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

WebDependencies.SetMiddleware(app);

app.Run();
