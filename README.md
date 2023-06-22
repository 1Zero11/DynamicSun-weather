# DynamicSun-weather

## Использовано
* ASP.NET Core MVC (.NET 6)
* Entity Framework Core
* PostgreSQL
* NPOI

## Запуск

1. Запустить postgres, создать юзера weather и бд weather, пароль password. Я делал через докер: 

`docker run -d --name dropbox-postgres -p 5432:5432 -e POSTGRES_DB=weather -e POSTGRES_USER=weather -e POSTGRES_PASSWORD=password postgres`

2. Перейти в папку с проектом и запустить `dotnet ef database update` чтобы создать структуру бд.
3. Запустить приложение

##
Для фильтрации нужно ввести год и\или выбрать месяц, а затем нажать "Фильтровать".


![изображение](https://github.com/1Zero11/DynamicSun-weather/assets/30704362/126cb508-09cc-4909-8ff0-b7b4a341c57b)


Постраничная навигация осуществляется с помощью кнопок внизу страницы.


![изображение](https://github.com/1Zero11/DynamicSun-weather/assets/30704362/4b578a3a-35b0-4a2d-9959-5bc3c579586d)

