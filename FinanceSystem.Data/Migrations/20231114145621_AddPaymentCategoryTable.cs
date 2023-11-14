using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentCategory",
                table: "Payments",
                newName: "PaymentCategoryId");

            migrationBuilder.CreateTable(
                name: "PaymentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentCategoryId",
                table: "Payments",
                column: "PaymentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentCategories_PaymentCategoryId",
                table: "Payments",
                column: "PaymentCategoryId",
                principalTable: "PaymentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentCategories_PaymentCategoryId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentCategories");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentCategoryId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentCategoryId",
                table: "Payments",
                newName: "PaymentCategory");
        }
    }
}
