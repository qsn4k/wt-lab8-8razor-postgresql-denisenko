using Microsoft.Extensions.Configuration;
using Npgsql;

namespace wt_lab8_8razor_postgresql_denisenko.Data;

/// <summary>Получает строку подключения из Render DATABASE_URL или ConnectionStrings:DefaultConnection.</summary>
public static class DatabaseConnectionString
{
    public static string Resolve(IConfiguration configuration)
    {
        var databaseUrl = configuration["DATABASE_URL"];
        if (!string.IsNullOrWhiteSpace(databaseUrl))
            return FromDatabaseUrl(databaseUrl);

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(connectionString))
            return connectionString;

        throw new InvalidOperationException(
            "Не задано подключение к PostgreSQL. Укажите DATABASE_URL или ConnectionStrings__DefaultConnection.");
    }

    private static string FromDatabaseUrl(string databaseUrl)
    {
        if (!Uri.TryCreate(databaseUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != "postgres" && uri.Scheme != "postgresql"))
        {
            throw new InvalidOperationException(
                "Переменная DATABASE_URL должна содержать PostgreSQL URL вида postgres://user:password@host:5432/database.");
        }

        var userInfo = uri.UserInfo.Split(':', 2);
        if (userInfo.Length == 0 || string.IsNullOrWhiteSpace(userInfo[0]))
            throw new InvalidOperationException("В DATABASE_URL не указано имя пользователя PostgreSQL.");

        var databaseName = Uri.UnescapeDataString(uri.AbsolutePath.Trim('/'));
        if (string.IsNullOrWhiteSpace(databaseName))
            throw new InvalidOperationException("В DATABASE_URL не указано имя базы данных.");

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = databaseName,
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty,
            // Internal Render URL обычно работает без обязательного TLS;
            // внешний URL содержит sslmode=require и будет обработан ниже.
            SslMode = SslMode.Prefer
        };

        // Для внешнего Render URL параметр sslmode=require включает обязательный TLS;
        // внутренний URL без параметра использует Prefer.
        var query = uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in query)
        {
            var pair = item.Split('=', 2);
            if (pair.Length == 2 && pair[0].Equals("sslmode", StringComparison.OrdinalIgnoreCase))
            {
                var sslMode = Uri.UnescapeDataString(pair[1]);
                if (Enum.TryParse<SslMode>(sslMode, true, out var parsedMode))
                    connectionStringBuilder.SslMode = parsedMode;
            }
        }

        return connectionStringBuilder.ConnectionString;
    }
}
