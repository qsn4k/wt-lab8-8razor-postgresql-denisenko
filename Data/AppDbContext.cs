using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Data;

/// <summary>Контекст Entity Framework Core для общей базы данных ЛР7 и ЛР8.</summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Таблица Players уже создана миграциями ЛР7.
        // Здесь только описываем её существующую структуру, не создаём повторные seed-записи.
        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Players");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nickname).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Game).HasMaxLength(60).IsRequired();
            entity.Property(x => x.TeamName)
                .HasMaxLength(60)
                .HasDefaultValue("Без команды")
                .IsRequired();
            entity.Property(x => x.Description)
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty)
                .IsRequired();
            // В ЛР7 CreatedAt имеет тип PostgreSQL timestamp with time zone.
            entity.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
        });

        // Таблица Tournaments — новая часть ЛР8, её создаёт миграция CreateTournaments.
        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.ToTable("Tournaments");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Game).HasMaxLength(60).IsRequired();
            entity.Property(x => x.Status).HasMaxLength(30).IsRequired();
            entity.Property(x => x.PrizePool).HasPrecision(12, 2);
            entity.Property(x => x.StartDate).HasColumnType("timestamp without time zone");

            entity.HasData(
                new Tournament { Id = 1, Name = "Cyber Cup: Autumn Split", Game = "Counter-Strike 2", StartDate = new DateTime(2026, 10, 24, 16, 0, 0), CurrentPlayers = 18, MaxPlayers = 32, PrizePool = 500m, Status = "Предстоящий" },
                new Tournament { Id = 2, Name = "Dota Arena: Season 4", Game = "Dota 2", StartDate = new DateTime(2026, 10, 26, 18, 0, 0), CurrentPlayers = 12, MaxPlayers = 16, PrizePool = 350m, Status = "Предстоящий" },
                new Tournament { Id = 3, Name = "Neon Masters", Game = "Valorant", StartDate = new DateTime(2026, 10, 28, 17, 0, 0), CurrentPlayers = 24, MaxPlayers = 32, PrizePool = 750m, Status = "Предстоящий" },
                new Tournament { Id = 4, Name = "Night League", Game = "Counter-Strike 2", StartDate = new DateTime(2026, 10, 19, 19, 0, 0), CurrentPlayers = 14, MaxPlayers = 16, PrizePool = 250m, Status = "Идёт" },
                new Tournament { Id = 5, Name = "Autumn Finals", Game = "Dota 2", StartDate = new DateTime(2026, 10, 12, 15, 0, 0), CurrentPlayers = 16, MaxPlayers = 16, PrizePool = 1000m, Status = "Завершён" }
            );
        });
    }
}
