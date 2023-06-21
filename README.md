# DynamicSun-weather

## Запуск

1. Запустить postgres, создать юзера dropbox и бд weather, пароль password. Я делал через докер: 

`docker run -d --name dropbox-postgres -p 5432:5432 -e POSTGRES_DB=weather -e POSTGRES_USER=weather -e POSTGRES_PASSWORD=password postgres`

2. Перейти в папку с проектом и запустить `dotnet ef database update` чтобы создать структуру бд.
3. Запустить приложение
