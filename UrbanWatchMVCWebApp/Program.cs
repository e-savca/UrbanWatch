using UrbanWatchMVCWebApp.Services;
using UrbanWatchMVCWebApp.Logging;
using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(connection);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();

});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"Logs/InfoLogs/{DateTime.Now.ToString("yyyy-MM-dd")}.log"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TranzyServiceLocal>();
builder.Services.AddScoped<TranzyServiceWebAPI>();
builder.Services.AddScoped<ITranzyAdapter, TranzyAdapter>();


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    await dbContext.OnApplicationStarted();
    await dbContext.UpdateDatabase();
}

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
