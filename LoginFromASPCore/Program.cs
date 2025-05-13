using LoginFromASPCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection String ke liye ek service banayege aur usko yaha pe register karwayege 
// jisse ki yaha per hamari constring register hojayegi
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();   // Generic method 
builder.Services.AddDbContext<CodeFirstDbContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));


builder.Services.AddSession(); // for session

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession(); // for session
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
