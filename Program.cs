using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;
using AuctionApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Register database
builder.Services.AddDbContext<AuctionDBContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));

builder.Services.AddDefaultIdentity<AppIdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppIdentityDbContext>();

// Dependency Injections
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionPersistence>();
    
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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();