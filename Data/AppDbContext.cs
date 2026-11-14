using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Data;

/// <summary>Контекст Entity Framework Core для работы с PostgreSQL.</summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Players");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nickname).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Game).HasMaxLength(60).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500).HasDefaultValue(string.Empty);
            entity.Property(x => x.CreatedAt).HasColumnType("timestamp without time zone");
            entity.HasIndex(x => x.Nickname);
            entity.HasData(
                new Player { Id = 1, Nickname = "CyberWolf", Game = "Counter-Strike 2", Rating = 1500, Wins = 24, Losses = 12, CreatedAt = new DateTime(2026, 9, 1) },
                new Player { Id = 2, Nickname = "ShadowFox", Game = "Counter-Strike 2", Rating = 1750, Wins = 31, Losses = 10, CreatedAt = new DateTime(2026, 9, 2) },
                new Player { Id = 3, Nickname = "NeoMaster", Game = "Dota 2", Rating = 2100, Wins = 38, Losses = 9, CreatedAt = new DateTime(2026, 9, 3) },
                new Player { Id = 4, Nickname = "DarkKnight", Game = "Dota 2", Rating = 1850, Wins = 29, Losses = 15, CreatedAt = new DateTime(2026, 9, 4) },
                new Player { Id = 5, Nickname = "PixelHunter", Game = "Valorant", Rating = 1600, Wins = 21, Losses = 18, CreatedAt = new DateTime(2026, 9, 5) },
                new Player { Id = 6, Nickname = "IceStorm", Game = "Valorant", Rating = 1900, Wins = 34, Losses = 11, CreatedAt = new DateTime(2026, 9, 6) },
                new Player { Id = 7, Nickname = "RedDragon", Game = "Counter-Strike 2", Rating = 1250, Wins = 15, Losses = 20, CreatedAt = new DateTime(2026, 9, 7) }
            );
        });

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
