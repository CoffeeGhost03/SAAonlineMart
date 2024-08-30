using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAAonlineMart.Areas.Identity.Data;
using SAAonlineMart.Models;
using System.Reflection.Emit;

namespace SAAonlineMart.Data;

public class SAAonlineMartContext : IdentityDbContext<SAAonlineMartUser>
{
    public SAAonlineMartContext(DbContextOptions<SAAonlineMartContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Products>().HasData(
        new Products { ProductId = 1, ProductName = "Shoe", ProductPrice = 1200.0m, URL = "https://m.media-amazon.com/images/I/81XOtXza9NS._AC_SX679_.jpg" }
);
    }

public DbSet<SAAonlineMart.Models.Products> Products { get; set; } = default!;

public DbSet<SAAonlineMart.Models.Order> Order { get; set; } = default!;
}
