using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAAonlineMart.Data;
using SAAonlineMart.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SAAonlineMartContextConnection") ?? throw new InvalidOperationException("Connection string 'SAAonlineMartContextConnection' not found.");


builder.Services.AddDbContext<SAAonlineMartContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<SAAonlineMartUser, IdentityRole>()
    .AddEntityFrameworkStores<SAAonlineMartContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SAAonlineMartContext>();
        await context.Database.MigrateAsync(); // Apply any pending migrations

        // Seed the admin user and roles
        await ApplicationDbInitializer.SeedAdminUser(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
