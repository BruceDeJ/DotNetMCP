using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "Alice", "Johnson" },
                    { 2, "bob@example.com", "Bob", "Smith" },
                    { 3, "carol@example.com", "Carol", "Davis" },
                    { 4, "david@example.com", "David", "Miller" },
                    { 5, "eve@example.com", "Eve", "Wilson" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Basic widget", "Widget A", 9.99m },
                    { 2, "Advanced widget", "Widget B", 19.99m },
                    { 3, "Useful gadget", "Gadget C", 14.50m },
                    { 4, "Another gadget", "Gadget D", 24.00m },
                    { 5, "Handy thing", "Thingamajig", 4.75m },
                    { 6, "Small doodad", "Doodad", 7.30m },
                    { 7, "Mysterious whatsit", "Whatsit", 12.00m },
                    { 8, "Complex doohickey", "Doohickey", 29.99m },
                    { 9, "Multi-part contraption", "Contraption", 49.99m },
                    { 10, "Handheld gizmo", "Gizmo", 5.25m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "Total" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 29.98m },
                    { 2, 1, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.99m },
                    { 3, 2, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 14.5m },
                    { 4, 3, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.75m },
                    { 5, 4, new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.99m },
                    { 6, 5, new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.0m },
                    { 7, 2, new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9.99m },
                    { 8, 3, new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 24.0m },
                    { 9, 4, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 74.99m },
                    { 10, 5, new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.5m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 9.99m },
                    { 2, 2, 2, 1, 19.99m },
                    { 3, 3, 3, 1, 14.5m },
                    { 4, 4, 5, 1, 4.75m },
                    { 5, 5, 8, 1, 29.99m },
                    { 6, 5, 6, 4, 6.0m },
                    { 7, 6, 7, 1, 12.0m },
                    { 8, 7, 1, 1, 9.99m },
                    { 9, 8, 4, 1, 24.0m },
                    { 10, 9, 9, 1, 49.99m },
                    { 11, 9, 10, 1, 24.999m },
                    { 12, 10, 3, 1, 14.5m },
                    { 13, 10, 10, 1, 8.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
