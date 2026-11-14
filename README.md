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

> Методичка WT-8 показывает пример с `Product`/`Products`. В твоём исходном WT-5 используется предметная область Tournament Hub, поэтому пример перенесён на реальные сущности проекта: `Player` и `Tournament`. Задача `AddProductDescription` выполнена как `AddPlayerDescription` — отдельная миграция добавляет `Description` к профилю игрока.

## Требования

- .NET SDK 9.0;
- PostgreSQL 14 или новее;
- Docker — для проверки контейнера;
- Git и аккаунты GitHub/Render — для публикации.

## 1. Подготовка PostgreSQL

Создай базу данных `wt_lab8` (например, в pgAdmin или psql):

```sql
CREATE DATABASE wt_lab8;
```

Скопируй `appsettings.Development.example.json` в `appsettings.Development.json` и укажи локальный пароль PostgreSQL. Этот локальный файл добавлен в `.gitignore` и не должен отправляться на GitHub.

Пример строки подключения:

```text
Host=localhost;Port=5432;Database=wt_lab8;Username=postgres;Password=YOUR_LOCAL_PASSWORD
```

Либо задай переменную окружения `ConnectionStrings__DefaultConnection`.

## 2. Установка инструментов и восстановление пакетов

Открой терминал в каталоге проекта:

```powershell
dotnet tool restore
dotnet restore
```

В проекте используется локальный манифест `dotnet-ef` версии 9.0.0.

## 3. Применение миграций

```powershell
dotnet ef migrations list
dotnet ef database update
```

Должны примениться:

1. `InitialCreate` — создаёт таблицы `Players`, `Tournaments` и стартовые записи;
2. `AddPlayerDescription` — добавляет поле `Description` в таблицу `Players`.

Проверить таблицы можно SQL-запросами:

```sql
SELECT * FROM "Players" ORDER BY "Id";
SELECT * FROM "Tournaments" ORDER BY "Id";
SELECT * FROM "__EFMigrationsHistory" ORDER BY "MigrationId";
```

Проверка структуры:

```sql
SELECT column_name, data_type
FROM information_schema.columns
WHERE table_name = 'Players'
ORDER BY ordinal_position;
```

## 4. Запуск приложения

```powershell
dotnet run
```

Приложение слушает порт из переменной `PORT`; локально по умолчанию это `10000`:

- главная: `http://localhost:10000/`;
- игроки: `http://localhost:10000/Players`;
- турниры: `http://localhost:10000/Tournaments`.

При старте выполняется `Database.Migrate()`, поэтому приложение применяет доступные миграции к указанной базе данных.

## 5. Проверка CRUD и поиска

1. Открой список игроков и создай нового игрока.
2. Открой карточку созданного игрока, измени рейтинг и описание.
3. Удали тестового игрока.
4. Выполни поиск по части никнейма и фильтр по игре.
5. Перезапусти приложение и проверь, что данные сохранились.
6. Открой турниры и проверь поиск и фильтры.

## 6. Docker

```powershell
docker build -t wt-lab8-razor-postgresql .
docker run --rm -p 10000:10000 `
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=wt_lab8;Username=postgres;Password=YOUR_LOCAL_PASSWORD" `
  wt-lab8-razor-postgresql
```

В Docker Desktop на Windows `host.docker.internal` позволяет контейнеру обращаться к PostgreSQL на хост-машине. Если БД находится в другом контейнере, используй имя сервиса в качестве `Host` и подключи контейнеры к общей Docker-сети.

## 7. Публикация на GitHub и Render

### GitHub

Создай отдельный репозиторий для WT-8, затем выполни:

```powershell
git init
git add .
git commit -m "WT-8: PostgreSQL and Entity Framework Core"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/wt-lab8-8razor-postgresql-denisenko.git
git push -u origin main
```

Перед `git push` убедись, что локальный пароль находится только в `appsettings.Development.json`, а этот файл игнорируется Git.

### Render

1. Создай PostgreSQL Database на Render.
2. Создай Web Service из GitHub-репозитория.
3. Выбери Runtime **Docker**.
4. В Environment добавь переменную `ConnectionStrings__DefaultConnection` со строкой подключения к Render PostgreSQL. Используй Internal Database URL host/port/db/user/password в формате Npgsql:

```text
Host=RENDER_DB_HOST;Port=5432;Database=RENDER_DB_NAME;Username=RENDER_DB_USER;Password=RENDER_DB_PASSWORD;SSL Mode=Require;Trust Server Certificate=true
```

5. Запусти Deploy и проверь логи. На старте приложения EF Core применит миграции к удалённой базе.
6. Открой Render URL, проверь список игроков, создание/редактирование/удаление и поиск.

Никогда не помещай реальный пароль БД, токены и секреты в код, README, отчёт или GitHub.

## 8. Ссылки публикации — заполнить после Deploy

- **GitHub:** `ВСТАВИТЬ_ССЫЛКУ_НА_РЕПОЗИТОРИЙ`
- **Render:** `ВСТАВИТЬ_ССЫЛКУ_НА_ОПУБЛИКОВАННОЕ_ПРИЛОЖЕНИЕ`

Дополнительные пустые поля находятся в файле `PUBLICATION_LINKS.md`.

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
