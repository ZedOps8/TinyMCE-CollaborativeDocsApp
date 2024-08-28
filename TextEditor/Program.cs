using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TextEditor.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get the connection string for the database from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register the DbContext with the dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add a filter for developer exception pages
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity services with default settings and link to the ApplicationDbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services for controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    // Use the migrations endpoint for database migrations
    app.UseMigrationsEndPoint();
}
else
{
    // Use a global exception handler for production
    app.UseExceptionHandler("/Home/Error");
    // Enforce HTTPS Strict Transport Security (HSTS) for production
    // The default HSTS value is 30 days. You may want to adjust this value.
    app.UseHsts();
}

// Middleware for HTTPS redirection
app.UseHttpsRedirection();

// Serve static files (e.g., CSS, JavaScript, images)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable authorization middleware
app.UseAuthorization();

// Configure endpoint routing for controllers and Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Docs}/{action=Index}/{id?}"); // Default route pattern
app.MapRazorPages(); // Map Razor Pages endpoints

// Run the application
app.Run();
