using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAAonlineMart.Migrations
{
    /// <inheritdoc />
    public partial class sinc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "ProductName", "ProductPrice", "URL" },
                values: new object[] { "Sennheiser Cable", 400m, "https://www.e-piphany.co.za/cdn/shop/files/Sennheiser-Spare-Connecting-Cable-for-HD-465.jpg?v=1691433981" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName", "ProductPrice", "URL" },
                values: new object[,]
                {
                    { 2, "Earpads", 500m, "https://www.e-piphany.co.za/cdn/shop/files/S-SEN-572287.jpg?v=1691438130" },
                    { 3, "Sennheiser HD560S", 3000m, "https://www.e-piphany.co.za/cdn/shop/files/SENNHEISER-HD-560S.jpg?v=1691429397" },
                    { 4, "Headphone Stand", 200m, "https://thumbs.static-thomann.de/thumb/padthumb600x600/pics/bdb/_27/276751/5252391_800.jpg" },
                    { 5, "Sennheiser HD559", 2900m, "https://www.e-piphany.co.za/cdn/shop/files/product_detail_x2_desktop_HD_559_Sennheiser_01.jpg?v=1691423669" },
                    { 6, "Headphone Case", 300m, "https://media.takealot.com/covers_tsins/56002877/5055261857431-3-pdpxl.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "ProductName", "ProductPrice", "URL" },
                values: new object[] { "Shoe", 1200.0m, "https://m.media-amazon.com/images/I/81XOtXza9NS._AC_SX679_.jpg" });
        }
    }
}
