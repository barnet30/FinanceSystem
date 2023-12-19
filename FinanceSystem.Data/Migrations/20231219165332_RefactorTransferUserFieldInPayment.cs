using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTransferUserFieldInPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_TransferUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_TransferUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TransferUserId",
                table: "Payments");

            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "TransferUserId",
                table: "Payments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransferUserId",
                table: "Payments",
                column: "TransferUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_TransferUserId",
                table: "Payments",
                column: "TransferUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
