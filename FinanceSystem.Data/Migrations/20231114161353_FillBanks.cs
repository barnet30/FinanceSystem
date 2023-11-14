using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FillBanks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM ""Banks"";

            insert into ""Banks"" (""Id"",""Name"")
            values (1,'Сбербанк'),
            (2,'Тинькофф'),
            (3,'ВТБ'),
            (4,'Альфабанк'),
            (5,'Совкомбанк');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
