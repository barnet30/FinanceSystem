using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Banks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Banks",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
