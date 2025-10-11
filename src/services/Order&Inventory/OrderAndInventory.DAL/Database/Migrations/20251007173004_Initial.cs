using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderAndInventory.DAL.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    inventory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_quantity = table.Column<int>(type: "integer", nullable: false),
                    reorder_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventories", x => x.inventory_id);
                    table.CheckConstraint("chk_inventories_reorder_level_greater_than_zero", "reorder_level > 0");
                    table.CheckConstraint("chk_inventories_stock_quantity_greater_than_zero", "stock_quantity > 0");
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.member_id);
                });

            migrationBuilder.CreateTable(
                name: "staves",
                columns: table => new
                {
                    staff_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_staves", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.order_id);
                    table.CheckConstraint("chk_orders_order_date_not_future", "order_date <= NOW()");
                    table.ForeignKey(
                        name: "fk_orders_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "member_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.order_item_id);
                    table.CheckConstraint("chk_order_items_quantity_greater_than_zero", "quantity > 0");
                    table.CheckConstraint("chk_order_items_unit_price_greater_than_zero", "unit_price > 0");
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    paid_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    payment_method = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.payment_id);
                    table.CheckConstraint("chk_payments_amount_greater_than_zero", "amount > 0");
                    table.CheckConstraint("chk_payments_paid_date_not_future", "paid_date <= Now()");
                    table.ForeignKey(
                        name: "fk_payments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff_orders",
                columns: table => new
                {
                    staff_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assigned_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_staff_orders", x => new { x.order_id, x.staff_id });
                    table.ForeignKey(
                        name: "fk_staff_orders_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_staff_orders_staves_staff_id",
                        column: x => x.staff_id,
                        principalTable: "staves",
                        principalColumn: "staff_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "inventories",
                columns: new[] { "inventory_id", "book_id", "reorder_level", "stock_quantity" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), 20, 100 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("32222222-2222-2222-2222-222222222222"), 15, 50 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), 25, 75 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("54444444-4444-4444-4444-444444444444"), 10, 30 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("65555555-5555-5555-5555-555555555555"), 50, 200 }
                });

            migrationBuilder.InsertData(
                table: "members",
                columns: new[] { "member_id", "email", "first_name", "last_name", "phone_number" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "john.doe@example.com", "John", "Doe", "(555) 123-4567" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "jane.smith@example.com", "Jane", "Smith", "(555) 234-5678" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "alice.johnson@example.com", "Alice", "Johnson", "(555) 345-6789" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "bob.williams@example.com", "Bob", "Williams", "(555) 456-7890" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "emma.brown@example.com", "Emma", "Brown", "(555) 567-8901" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_members_email",
                table: "members",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_member_id",
                table: "orders",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_order_id",
                table: "payments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_staff_orders_staff_id",
                table: "staff_orders",
                column: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "staff_orders");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "staves");

            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
