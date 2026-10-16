using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRobot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_orders_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "incoming_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    external_message_id = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    storage_key = table.Column<string>(type: "text", nullable: true),
                    original_file_name = table.Column<string>(type: "text", nullable: true),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    purchase_order_id = table.Column<Guid>(type: "uuid", nullable: true),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    last_error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_incoming_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_incoming_documents_purchase_orders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_incoming_documents_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sku = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_order_lines", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_order_lines_purchase_orders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "incoming_document_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    incoming_document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sku = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_incoming_document_lines", x => x.id);
                    table.ForeignKey(
                        name: "fk_incoming_document_lines_incoming_documents_incoming_documen",
                        column: x => x.incoming_document_id,
                        principalTable: "incoming_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operator_questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    incoming_document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    answer = table.Column<string>(type: "text", nullable: true),
                    asked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    answered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operator_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_operator_questions_incoming_documents_incoming_document_id",
                        column: x => x.incoming_document_id,
                        principalTable: "incoming_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_incoming_document_lines_incoming_document_id",
                table: "incoming_document_lines",
                column: "incoming_document_id");

            migrationBuilder.CreateIndex(
                name: "ix_incoming_documents_channel_external_message_id",
                table: "incoming_documents",
                columns: new[] { "channel", "external_message_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_incoming_documents_purchase_order_id",
                table: "incoming_documents",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_incoming_documents_status",
                table: "incoming_documents",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_incoming_documents_supplier_id",
                table: "incoming_documents",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_operator_questions_incoming_document_id",
                table: "operator_questions",
                column: "incoming_document_id");

            migrationBuilder.CreateIndex(
                name: "ix_operator_questions_status",
                table: "operator_questions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_order_lines_purchase_order_id",
                table: "purchase_order_lines",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_number",
                table: "purchase_orders",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_supplier_id",
                table: "purchase_orders",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incoming_document_lines");

            migrationBuilder.DropTable(
                name: "operator_questions");

            migrationBuilder.DropTable(
                name: "purchase_order_lines");

            migrationBuilder.DropTable(
                name: "incoming_documents");

            migrationBuilder.DropTable(
                name: "purchase_orders");

            migrationBuilder.DropTable(
                name: "suppliers");
        }
    }
}
