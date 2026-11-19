using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = DatabaseConnectionString.Resolve(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// В учебном проекте миграции применяются при запуске, в том числе на Render.
// Чтобы отключить автоматическое применение, задайте APPLY_DATABASE_MIGRATIONS=false.
if (!string.Equals(Environment.GetEnvironmentVariable("APPLY_DATABASE_MIGRATIONS"), "false", StringComparison.OrdinalIgnoreCase))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
