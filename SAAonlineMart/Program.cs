using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAAonlineMart.Data;
using SAAonlineMart.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SAAonlineMartContextConnection") ?? throw new InvalidOperationException("Connection string 'SAAonlineMartContextConnection' not found.");

builder.Services.AddDbContext<SAAonlineMartContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SAAonlineMartUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SAAonlineMartContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
