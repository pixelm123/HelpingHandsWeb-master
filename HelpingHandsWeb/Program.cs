using HelpingHandsWeb.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Configure session services
builder.Services.AddSession(options =>
{
    // Set session timeout (optional)
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
});

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

// Use session middleware
app.UseSession();

// Custom routing for header items
// Custom routing for header items
app.MapControllerRoute(
    name: "about",
    pattern: "About",
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "contact",
    defaults: new { controller = "Home", action = "Contact" });

app.MapControllerRoute(
    name: "service",  // Unique route name for the "service" route
    pattern: "service",
    defaults: new { controller = "Home", action = "Service" });

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Register", action = "Register" });

app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Login", action = "Login" });



app.MapControllerRoute(
   name: "admin-dashboard",
   pattern: "AdminDashboard",
   defaults: new { controller = "Admin", action = "AdminDashboard" });



app.MapControllerRoute(
    name: "admin-patients",
    pattern: "Admin/Patients",
    defaults: new { controller = "Admin", action = "Patients" });

app.MapControllerRoute(
    name: "admin-nurses",
    pattern: "Admin/Nurses",
    defaults: new { controller = "Admin", action = "Nurses" });

app.MapControllerRoute(
    name: "admin-office-managers",
    pattern: "Admin/OfficeManagers",
    defaults: new { controller = "Admin", action = "OfficeManagers" });

app.MapControllerRoute(
    name: "admin-suburbs",
    pattern: "Admin/Suburbs",
    defaults: new { controller = "Admin", action = "Suburbs" });

app.MapControllerRoute(
    name: "profile-settings",
    pattern: "Profile/Settings",
    defaults: new { controller = "Profile", action = "Settings" });

app.MapControllerRoute(
    name: "admin-reports",
    pattern: "Admin/Reports",
    defaults: new { controller = "Admin", action = "Reports" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
