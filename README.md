# WT-8 — Tournament Hub + PostgreSQL + EF Core

Продолжение WT-5: исходный интерфейс Tournament Hub на Razor Pages сохранён, а временные хранилища `PlayerStore` и `TournamentStore` заменены на PostgreSQL через Entity Framework Core.

## Что реализовано

- подключение PostgreSQL через `Npgsql.EntityFrameworkCore.PostgreSQL`;
- общий `Data/AppDbContext` и регистрация через dependency injection;
- таблицы `Players` и `Tournaments`, индексы и стартовые данные;
- миграции `InitialCreate` и `AddPlayerDescription`;
- полный CRUD профилей игроков (создание, просмотр, редактирование, удаление);
- поиск по никнейму и фильтрация по игре через LINQ;
- сортировка рейтинга и вывод ТОП-3 из PostgreSQL;
- загрузка турниров, поиск и фильтры из PostgreSQL;
- проверка валидации моделей и сохранение изменений между перезапусками;
- применение миграций при запуске приложения (для учебной публикации на Render);
- Dockerfile и конфигурация порта `PORT`;
- безопасная настройка строки подключения через переменную окружения.

## Ссылки публикации

- **GitHub:** `ВСТАВИТЬ_ССЫЛКУ_НА_РЕПОЗИТОРИЙ`
- **Render:** `ВСТАВИТЬ_ССЫЛКУ_НА_ОПУБЛИКОВАННОЕ_ПРИЛОЖЕНИЕ`

## Структура проекта

```text
wt-lab8-8razor-postgresql-denisenko/
├── Data/
│   ├── AppDbContext.cs
│   └── AppDbContextFactory.cs
├── Migrations/
│   ├── 20261114000100_InitialCreate.cs
│   ├── 20261114000200_AddPlayerDescription.cs
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   ├── Player.cs
│   └── Tournament.cs
├── Pages/
│   ├── Players/
│   ├── Tournaments/
│   └── ...
├── wwwroot/
├── .config/dotnet-tools.json
├── Dockerfile
├── Program.cs
├── appsettings.json
└── wt-lab8-8razor-postgresql-denisenko.csproj
```
