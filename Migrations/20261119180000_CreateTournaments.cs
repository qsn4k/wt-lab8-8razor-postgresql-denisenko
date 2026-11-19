using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace wt_lab8_8razor_postgresql_denisenko.Migrations;

/// <inheritdoc />
public partial class CreateTournaments : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Players уже существует в базе ЛР7. Эту таблицу не пересоздаём.
        migrationBuilder.CreateTable(
            name: "Tournaments",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                Game = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                CurrentPlayers = table.Column<int>(type: "integer", nullable: false),
                MaxPlayers = table.Column<int>(type: "integer", nullable: false),
                PrizePool = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Tournaments", x => x.Id));

        migrationBuilder.InsertData(
            table: "Tournaments",
            columns: new[] { "Id", "CurrentPlayers", "Game", "MaxPlayers", "Name", "PrizePool", "StartDate", "Status" },
            values: new object[,]
            {
                { 1, 18, "Counter-Strike 2", 32, "Cyber Cup: Autumn Split", 500m, new DateTime(2026, 10, 24, 16, 0, 0, DateTimeKind.Unspecified), "Предстоящий" },
                { 2, 12, "Dota 2", 16, "Dota Arena: Season 4", 350m, new DateTime(2026, 10, 26, 18, 0, 0, DateTimeKind.Unspecified), "Предстоящий" },
                { 3, 24, "Valorant", 32, "Neon Masters", 750m, new DateTime(2026, 10, 28, 17, 0, 0, DateTimeKind.Unspecified), "Предстоящий" },
                { 4, 14, "Counter-Strike 2", 16, "Night League", 250m, new DateTime(2026, 10, 19, 19, 0, 0, DateTimeKind.Unspecified), "Идёт" },
                { 5, 16, "Dota 2", 16, "Autumn Finals", 1000m, new DateTime(2026, 10, 12, 15, 0, 0, DateTimeKind.Unspecified), "Завершён" }
            });

        // В ЛР7 стартовые игроки вставлялись с явными Id. Синхронизируем identity,
        // чтобы первая новая запись получила следующий свободный номер.
        migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Players\"', 'Id'), COALESCE((SELECT MAX(\"Id\") FROM \"Players\"), 1), true);");
        migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Tournaments\"', 'Id'), COALESCE((SELECT MAX(\"Id\") FROM \"Tournaments\"), 1), true);");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Откат миграции ЛР8 удаляет только добавленную ЛР8 таблицу.
        migrationBuilder.DropTable(name: "Tournaments");
    }
}
