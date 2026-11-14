using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace wt_lab8_8razor_postgresql_denisenko.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
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
            b.Property<string>("Description").IsRequired().HasMaxLength(500).HasDefaultValue("").HasColumnType("character varying(500)");
            b.Property<string>("Game").IsRequired().HasMaxLength(60).HasColumnType("character varying(60)");
            b.Property<string>("Nickname").IsRequired().HasMaxLength(30).HasColumnType("character varying(30)");
            b.Property<int>("Losses").HasColumnType("integer");
            b.Property<int>("Rating").HasColumnType("integer");
            b.Property<int>("Wins").HasColumnType("integer");
            b.HasKey("Id");
            b.HasIndex("Nickname");
            b.ToTable("Players");
            b.HasData(
                new { Id = 1, CreatedAt = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Counter-Strike 2", Losses = 12, Nickname = "CyberWolf", Rating = 1500, Wins = 24 },
                new { Id = 2, CreatedAt = new DateTime(2026, 9, 2, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Counter-Strike 2", Losses = 10, Nickname = "ShadowFox", Rating = 1750, Wins = 31 },
                new { Id = 3, CreatedAt = new DateTime(2026, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Dota 2", Losses = 9, Nickname = "NeoMaster", Rating = 2100, Wins = 38 },
                new { Id = 4, CreatedAt = new DateTime(2026, 9, 4, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Dota 2", Losses = 15, Nickname = "DarkKnight", Rating = 1850, Wins = 29 },
                new { Id = 5, CreatedAt = new DateTime(2026, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Valorant", Losses = 18, Nickname = "PixelHunter", Rating = 1600, Wins = 21 },
                new { Id = 6, CreatedAt = new DateTime(2026, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Valorant", Losses = 11, Nickname = "IceStorm", Rating = 1900, Wins = 34 },
                new { Id = 7, CreatedAt = new DateTime(2026, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), Description = "", Game = "Counter-Strike 2", Losses = 20, Nickname = "RedDragon", Rating = 1250, Wins = 15 }
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
}
