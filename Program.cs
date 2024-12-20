using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerBrowser;
using ServerBrowser.Data;
using ServerBrowser.Services;
using ServerBrowser.Services.Contracts;
using System.Collections.Generic;

// Start the listen service
// var ServerListener = new Thread(new ThreadStart(Global.Listen));
// ServerListener.Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Services
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
builder.Services.AddScoped<IListService, ListService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Error/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
