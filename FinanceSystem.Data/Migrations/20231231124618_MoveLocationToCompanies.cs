using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveLocationToCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Locations_LocationId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LocationId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Locations_LocationId",
                table: "Companies",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Locations_LocationId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LocationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Payments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LocationId",
                table: "Payments",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Locations_LocationId",
                table: "Payments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
