using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class ProductsInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Descriprion", "ImagePath", "Name" },
                values: new object[] { 1, 1000m, "Описание1", "/img/1.jpg", "Продукт1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Descriprion", "ImagePath", "Name" },
                values: new object[] { 2, 2000m, "Описание2", "/img/2.jpg", "Продукт2" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Descriprion", "ImagePath", "Name" },
                values: new object[] { 3, 3000m, "Описание3", "/img/3.jpg", "Продукт3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
