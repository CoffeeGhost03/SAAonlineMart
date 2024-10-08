﻿using Microsoft.AspNetCore.Identity;
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
        builder.Entity<Products>().HasData(
        new Products { ProductId = 1, ProductName = "Sennheiser Cable", ProductPrice = 400m, URL = "https://www.e-piphany.co.za/cdn/shop/files/Sennheiser-Spare-Connecting-Cable-for-HD-465.jpg?v=1691433981" },
        new Products { ProductId = 2, ProductName = "Earpads", ProductPrice = 500m, URL = "https://www.e-piphany.co.za/cdn/shop/files/S-SEN-572287.jpg?v=1691438130" },
        new Products { ProductId = 3, ProductName = "Sennheiser HD560S", ProductPrice = 3000m, URL = "https://www.e-piphany.co.za/cdn/shop/files/SENNHEISER-HD-560S.jpg?v=1691429397" },
        new Products { ProductId = 4, ProductName = "Headphone Stand", ProductPrice = 200m, URL = "https://thumbs.static-thomann.de/thumb/padthumb600x600/pics/bdb/_27/276751/5252391_800.jpg" },
        new Products { ProductId = 5, ProductName = "Sennheiser HD559", ProductPrice = 2900m, URL = "https://www.e-piphany.co.za/cdn/shop/files/product_detail_x2_desktop_HD_559_Sennheiser_01.jpg?v=1691423669" },
        new Products { ProductId = 6, ProductName = "Headphone Case", ProductPrice = 300m, URL = "https://media.takealot.com/covers_tsins/56002877/5055261857431-3-pdpxl.jpeg" }
        );
    }

    public DbSet<SAAonlineMart.Models.Products> Products { get; set; } = default!;

    public DbSet<SAAonlineMart.Models.Order> Order { get; set; } = default!;

    public DbSet<SAAonlineMart.Models.Order> OrderItems { get; set; } = default!;

}

public static class ApplicationDbInitializer
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<SAAonlineMartUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure the admin role exists
        await EnsureRoleExists(roleManager, "Admin");

        // Check if the admin user already exists
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");

        if (adminUser == null)
        {
            // Create the admin user
            adminUser = new SAAonlineMartUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin@123";

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                // Assign the user to the admin role
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                // Handle errors
                throw new Exception("Failed to create the admin user");
            }
        }
    }

    private static async Task EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
