using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warehouse_WarehouseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WarehouseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warehouse_WarehouseId",
                table: "Products",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }
    }
}
