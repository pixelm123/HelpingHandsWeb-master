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
    name: "service",  
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
    name: "changePassword",
    pattern: "Admin/change-password",
    defaults: new { controller = "Admin", action = "ChangePassword" });

app.MapControllerRoute(
    name: "profile",
    pattern: "Admin/profile",
    defaults: new { controller = "Admin", action = "Profile" });

app.MapControllerRoute(
    name: "addCity",
    pattern: "Admin/add-city",
    defaults: new { controller = "Admin", action = "AddCity" });

app.MapControllerRoute(
    name: "editCity",
    pattern: "Admin/edit-city",
    defaults: new { controller = "Admin", action = "EditCity" });

app.MapControllerRoute(
    name: "deleteCity",
    pattern: "Admin/delete-city/{id}",
    defaults: new { controller = "Admin", action = "DeleteCity" });

app.MapControllerRoute(
    name: "cities",
    pattern: "Admin/cities",
    defaults: new { controller = "Admin", action = "Cities" });

app.MapControllerRoute(
    name: "addSuburb",
    pattern: "Admin/add-suburb",
    defaults: new { controller = "Admin", action = "AddSuburb" });

app.MapControllerRoute(
    name: "editSuburb",
    pattern: "Admin/edit-suburb",
    defaults: new { controller = "Admin", action = "EditSuburb" });

app.MapControllerRoute(
    name: "deleteSuburb",
    pattern: "Admin/delete-suburb/{id}",
    defaults: new { controller = "Admin", action = "DeleteSuburb" });

app.MapControllerRoute(
    name: "suburbs",
    pattern: "Admin/suburbs",
    defaults: new { controller = "Admin", action = "Suburbs" });

app.MapControllerRoute(
    name: "conditions",
    pattern: "Admin/conditions",
    defaults: new { controller = "Admin", action = "Conditions" });

app.MapControllerRoute(
    name: "addCondition",
    pattern: "Admin/add-condition",
    defaults: new { controller = "Admin", action = "AddCondition" });

app.MapControllerRoute(
    name: "editCondition",
    pattern: "Admin/edit-condition/{id}",
    defaults: new { controller = "Admin", action = "EditCondition" });

app.MapControllerRoute(
    name: "deleteCondition",
    pattern: "Admin/delete-condition/{id}",
    defaults: new { controller = "Admin", action = "DeleteCondition" });

app.MapControllerRoute(
    name: "addOfficeManager",
    pattern: "Admin/add-officemanager",
    defaults: new { controller = "Admin", action = "AddOfficeManager" });

app.MapControllerRoute(
    name: "editOfficeManager",
    pattern: "Admin/edit-officemanager",
    defaults: new { controller = "Admin", action = "EditOfficeManager" });

app.MapControllerRoute(
    name: "officeManagers",
    pattern: "Admin/officemanagers",
    defaults: new { controller = "Admin", action = "OfficeManagers" });

app.MapControllerRoute(
    name: "addNurse",
    pattern: "Admin/add-nurse",
    defaults: new { controller = "Admin", action = "AddNurse" });

app.MapControllerRoute(
    name: "editNurse",
    pattern: "Admin/edit-nurse",
    defaults: new { controller = "Admin", action = "EditNurse" });

app.MapControllerRoute(
    name: "deleteNurse",
    pattern: "Admin/delete-nurse/{id}",
    defaults: new { controller = "Admin", action = "DeleteNurse" });

app.MapControllerRoute(
    name: "nurses",
    pattern: "Admin/nurses",
    defaults: new { controller = "Admin", action = "Nurses" });

app.MapControllerRoute(
    name: "patients",
    pattern: "Admin/patients",
    defaults: new { controller = "Admin", action = "Patients" });



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
