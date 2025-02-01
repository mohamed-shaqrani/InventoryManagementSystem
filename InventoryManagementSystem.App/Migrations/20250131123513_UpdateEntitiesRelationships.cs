using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StockTransactionDetails_StockTransactionDetailsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_Users_UserId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_Warehouse_WarehouseId",
                table: "StockTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactionDetails_UserId",
                table: "StockTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockTransactionDetailsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "StockTransactionDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StockTransactionDetails");

            migrationBuilder.DropColumn(
                name: "StockTransactionDetailsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "StockTransactionDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactionDetails_WarehouseId",
                table: "StockTransactionDetails",
                newName: "IX_StockTransactionDetails_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_UserId",
                table: "StockTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_Products_ProductId",
                table: "StockTransactionDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Users_UserId",
                table: "StockTransactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Warehouse_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_Products_ProductId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Users_UserId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Warehouse_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_UserId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "StockTransactions");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "StockTransactionDetails",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactionDetails_ProductId",
                table: "StockTransactionDetails",
                newName: "IX_StockTransactionDetails_WarehouseId");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "StockTransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StockTransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockTransactionDetailsId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_UserId",
                table: "StockTransactionDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockTransactionDetailsId",
                table: "Products",
                column: "StockTransactionDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StockTransactionDetails_StockTransactionDetailsId",
                table: "Products",
                column: "StockTransactionDetailsId",
                principalTable: "StockTransactionDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_Users_UserId",
                table: "StockTransactionDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_Warehouse_WarehouseId",
                table: "StockTransactionDetails",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
