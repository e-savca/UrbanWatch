using UrbanWatchMVCWebApp.Services;
using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UrbanWatchService>();
builder.Services.AddScoped<LeafletJSService>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddSingleton<IDataProviderService, TranzyDataProviderService>();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddHostedService<UrbanWatchBackgroundService>();




// Check if the database is being used based on the configuration in the configuration file
bool useDatabase = builder.Configuration.GetSection("DatabaseSettings")?.GetValue<bool>("UseDatabase") ?? false;
if (useDatabase)
{
    // Database is being used

    // Get the connection string from the configuration file
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add the database context service to the dependency container
    builder.Services.AddSingleton<ApplicationContext>(provider =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer(connection);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        return new ApplicationContext(optionsBuilder.Options);
    });


    // Add the data integration service
    builder.Services.AddSingleton<IDataIntegrationService, DatabaseIntegrationService>();    
}
else
{
    // No database is being used
    builder.Services.AddSingleton<IDataIntegrationService, MemoryIntegrationService>();
}


var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

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
