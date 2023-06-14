using UrbanWatchMVCWebApp.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UrbanWatchMVCWebApp.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"Logs/InfoLogs/{DateTime.Now.ToString("yyyy-MM-dd")}.log"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TranzyServiceLocal>();
builder.Services.AddScoped<TranzyServiceWebAPI>();
builder.Services.AddScoped<ITranzyAdapter, TranzyAdapter>();


var app = builder.Build();

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
