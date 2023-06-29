using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.IServices;
using UrbanWatchMVCWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UrbanWatchService>();
builder.Services.AddScoped<LeafletJSService>();

builder.Services.AddSingleton<IDataProviderService, TranzyDataProviderService>();
builder.Services.AddSingleton<MappingService>();
builder.Services.AddHostedService<UrbanWatchBackgroundService>();


// Check if the database is being used based on the configuration in the configuration file
var useDatabase = builder.Configuration.GetSection("DatabaseSettings")?.GetValue<bool>("UseDatabase") ?? false;
Extensions.UseDatabase = useDatabase;

if (useDatabase)
{
    // Database is being used

    // Get the connection string from the configuration file
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add the database context service to the dependency container
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

    // Add the data integration service
    builder.Services.AddSingleton<IDataIntegrationService, DatabaseIntegrationService>();
    builder.Services.AddScoped<IRepository, EFRepository>();
    builder.Services.AddSingleton<RoutesDataSnapshot>();
}
else
{
    // No database is being used
    builder.Services.AddSingleton<IDataIntegrationService, MemoryIntegrationService>();
    builder.Services.AddScoped<IRepository, DataContextRepository>();
    builder.Services.AddSingleton<FullDataSnapshot>();
}

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"]);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{   
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
