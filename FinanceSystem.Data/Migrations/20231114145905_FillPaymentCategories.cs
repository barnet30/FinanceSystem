using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FillPaymentCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM ""PaymentCategories"";

            insert into ""PaymentCategories"" (""Id"",""Name"")
            values (1,'Рестораны и кафе'),
            (2,'Онлайн магазины'),
            (3,'Транспорт'),
            (4,'Супермаркеты'),
            (5,'Здоровье и красота'),
            (6,'Коммуникация и интернет'),
            (7,'Спорт'),
            (8,'Развлечения и хобби'),
            (9,'Образование'),
            (10,'Одежда'),
            (11,'Налоги и штрафы'),
            (12,'Подписки на сторонние сервисы'),
            (13,'Другое');
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
