using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace wt_lab8_8razor_postgresql_denisenko.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261114000100_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Players",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nickname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                Game = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                Rating = table.Column<int>(type: "integer", nullable: false),
                Wins = table.Column<int>(type: "integer", nullable: false),
                Losses = table.Column<int>(type: "integer", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Players", x => x.Id));

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

        migrationBuilder.CreateIndex(name: "IX_Players_Nickname", table: "Players", column: "Nickname");

        migrationBuilder.InsertData(
            table: "Players",
            columns: new[] { "Id", "CreatedAt", "Game", "Losses", "Nickname", "Rating", "Wins" },
            values: new object[,]
            {
                { 1, new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2", 12, "CyberWolf", 1500, 24 },
                { 2, new DateTime(2026, 9, 2, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2", 10, "ShadowFox", 1750, 31 },
                { 3, new DateTime(2026, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), "Dota 2", 9, "NeoMaster", 2100, 38 },
                { 4, new DateTime(2026, 9, 4, 0, 0, 0, DateTimeKind.Unspecified), "Dota 2", 15, "DarkKnight", 1850, 29 },
                { 5, new DateTime(2026, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), "Valorant", 18, "PixelHunter", 1600, 21 },
                { 6, new DateTime(2026, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), "Valorant", 11, "IceStorm", 1900, 34 },
                { 7, new DateTime(2026, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2", 20, "RedDragon", 1250, 15 }
            });

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

        // Seed IDs are explicit; move identity sequences past the seeded rows.
        migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Players\"', 'Id'), COALESCE((SELECT MAX(\"Id\") FROM \"Players\"), 1), true);");
        migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Tournaments\"', 'Id'), COALESCE((SELECT MAX(\"Id\") FROM \"Tournaments\"), 1), true);");
    }


    // EF Core uses this model to map data operations such as InsertData.
    // It represents the schema immediately after InitialCreate (before Description was added).
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "9.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity<Player>(b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer");
            b.Property<DateTime>("CreatedAt").HasColumnType("timestamp without time zone");
            b.Property<string>("Game").IsRequired().HasMaxLength(60).HasColumnType("character varying(60)");
            b.Property<string>("Nickname").IsRequired().HasMaxLength(30).HasColumnType("character varying(30)");
            b.Property<int>("Losses").HasColumnType("integer");
            b.Property<int>("Rating").HasColumnType("integer");
            b.Property<int>("Wins").HasColumnType("integer");
            b.HasKey("Id");
            b.HasIndex("Nickname");
            b.ToTable("Players");
            b.HasData(
                new { Id = 1, CreatedAt = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), Game = "Counter-Strike 2", Losses = 12, Nickname = "CyberWolf", Rating = 1500, Wins = 24 },
                new { Id = 2, CreatedAt = new DateTime(2026, 9, 2, 0, 0, 0, DateTimeKind.Unspecified), Game = "Counter-Strike 2", Losses = 10, Nickname = "ShadowFox", Rating = 1750, Wins = 31 },
                new { Id = 3, CreatedAt = new DateTime(2026, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), Game = "Dota 2", Losses = 9, Nickname = "NeoMaster", Rating = 2100, Wins = 38 },
                new { Id = 4, CreatedAt = new DateTime(2026, 9, 4, 0, 0, 0, DateTimeKind.Unspecified), Game = "Dota 2", Losses = 15, Nickname = "DarkKnight", Rating = 1850, Wins = 29 },
                new { Id = 5, CreatedAt = new DateTime(2026, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), Game = "Valorant", Losses = 18, Nickname = "PixelHunter", Rating = 1600, Wins = 21 },
                new { Id = 6, CreatedAt = new DateTime(2026, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), Game = "Valorant", Losses = 11, Nickname = "IceStorm", Rating = 1900, Wins = 34 },
                new { Id = 7, CreatedAt = new DateTime(2026, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), Game = "Counter-Strike 2", Losses = 20, Nickname = "RedDragon", Rating = 1250, Wins = 15 }
            );
        });

        modelBuilder.Entity<Tournament>(b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer");
            b.Property<int>("CurrentPlayers").HasColumnType("integer");
            b.Property<string>("Game").IsRequired().HasMaxLength(60).HasColumnType("character varying(60)");
            b.Property<int>("MaxPlayers").HasColumnType("integer");
            b.Property<string>("Name").IsRequired().HasMaxLength(80).HasColumnType("character varying(80)");
            b.Property<decimal>("PrizePool").HasPrecision(12, 2).HasColumnType("numeric(12,2)");
            b.Property<DateTime>("StartDate").HasColumnType("timestamp without time zone");
            b.Property<string>("Status").IsRequired().HasMaxLength(30).HasColumnType("character varying(30)");
            b.HasKey("Id");
            b.ToTable("Tournaments");
            b.HasData(
                new { Id = 1, CurrentPlayers = 18, Game = "Counter-Strike 2", MaxPlayers = 32, Name = "Cyber Cup: Autumn Split", PrizePool = 500m, StartDate = new DateTime(2026, 10, 24, 16, 0, 0, DateTimeKind.Unspecified), Status = "Предстоящий" },
                new { Id = 2, CurrentPlayers = 12, Game = "Dota 2", MaxPlayers = 16, Name = "Dota Arena: Season 4", PrizePool = 350m, StartDate = new DateTime(2026, 10, 26, 18, 0, 0, DateTimeKind.Unspecified), Status = "Предстоящий" },
                new { Id = 3, CurrentPlayers = 24, Game = "Valorant", MaxPlayers = 32, Name = "Neon Masters", PrizePool = 750m, StartDate = new DateTime(2026, 10, 28, 17, 0, 0, DateTimeKind.Unspecified), Status = "Предстоящий" },
                new { Id = 4, CurrentPlayers = 14, Game = "Counter-Strike 2", MaxPlayers = 16, Name = "Night League", PrizePool = 250m, StartDate = new DateTime(2026, 10, 19, 19, 0, 0, DateTimeKind.Unspecified), Status = "Идёт" },
                new { Id = 5, CurrentPlayers = 16, Game = "Dota 2", MaxPlayers = 16, Name = "Autumn Finals", PrizePool = 1000m, StartDate = new DateTime(2026, 10, 12, 15, 0, 0, DateTimeKind.Unspecified), Status = "Завершён" }
            );
        });
#pragma warning restore 612, 618
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Players");
        migrationBuilder.DropTable(name: "Tournaments");
    }
}
