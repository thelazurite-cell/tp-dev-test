using DevTest.Interfaces;
using DevTest.Models;
using DevTest.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("Environment");
var selectedEnvironment = string.IsNullOrWhiteSpace(environment) ? "Development" : environment;
var location = AppDomain.CurrentDomain.BaseDirectory;
var configuration = new ConfigurationBuilder()
    .AddJsonFile(Path.Join(location, "appsettings.json"))
    .AddJsonFile(Path.Join(location, $"appsettings.{environment}.json"), optional: true)
    .Build();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddSingleton<IBlobContainerService, AzureBlobContainerService>();
builder.Services.AddSingleton<IFileUploadService, ImageUploadService>();
builder.Services.AddWebOptimizer(DevTest.Helpers.BundleHelper.RegisterBundles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseWebOptimizer();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
