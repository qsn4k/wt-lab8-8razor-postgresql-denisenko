# WT-8 — чек-лист проекта

## Реализовано в исходниках

- [x] Создана копия исходного WT-5 с сохранением интерфейса Tournament Hub.
- [x] Временные `PlayerStore` и `TournamentStore` удалены.
- [x] Создан `Data/AppDbContext.cs` с `DbSet<Player>` и `DbSet<Tournament>`.
- [x] Подключён провайдер `Npgsql.EntityFrameworkCore.PostgreSQL`.
- [x] Строка подключения читается из `ConnectionStrings:DefaultConnection`.
- [x] Подготовлена миграция `InitialCreate` для таблиц `Players` и `Tournaments`.
- [x] Подготовлена миграция `AddPlayerDescription` для добавления `Description` в `Players`.
- [x] Стартовые данные игроков и турниров переносятся через `HasData`.
- [x] Главная страница получает статистику и ТОП-3 из PostgreSQL.
- [x] Список игроков, фильтрация и поиск выполняются через EF Core/LINQ.
- [x] Создание, чтение, редактирование и удаление игроков используют PostgreSQL.
- [x] Страница турниров читает данные и выполняет поиск/фильтрацию через PostgreSQL.
- [x] Добавлено поле описания игрока и отображение его в карточке.
- [x] Миграции автоматически применяются при запуске приложения.
- [x] Добавлены Dockerfile, настройка `PORT` и инструкция по Render.
- [x] Добавлены поля для ссылок GitHub и Render в `README.md` и `PUBLICATION_LINKS.md`.
- [x] Локальные файлы с паролями исключены через `.gitignore` и `.dockerignore`.

## Что нужно выполнить на компьютере перед сдачей

- [ ] Скопировать `appsettings.Development.example.json` в `appsettings.Development.json` и указать локальный пароль PostgreSQL.
- [ ] Выполнить `dotnet tool restore` и `dotnet restore`.
- [ ] Выполнить `dotnet ef migrations list` и `dotnet ef database update`.
- [ ] Выполнить `dotnet build -c Release` и `dotnet run`.
- [ ] Проверить CRUD, поиск, фильтры и сохранность данных после перезапуска.
- [ ] Проверить Docker-сборку.
- [ ] Запушить проект в отдельный GitHub-репозиторий WT-8 и заполнить ссылку.
- [ ] Создать PostgreSQL на Render, опубликовать Web Service через Docker и заполнить Render URL.
- [ ] Сделать фактические скриншоты таблиц, миграций и опубликованного приложения для отчёта.

## Ограничение проверки в среде подготовки

В среде создания архива отсутствуют .NET SDK и Docker CLI, а доступ к загрузке SDK недоступен. Поэтому `dotnet build`, запуск приложения, Docker-сборка и публикация здесь не выполнялись. Файлы проекта и миграции подготовлены, но окончательную компиляцию и подключение к реальному PostgreSQL необходимо проверить локально.
