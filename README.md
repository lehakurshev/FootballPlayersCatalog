# 📚 README для проекта «Каталог футболистов 3.0»

Добро пожаловать в проект «Каталог футболистов 3.0»! Это простейшее веб-приложение, разработанное с использованием C# и ASP.NET Core, которое позволяет пользователям добавлять и управлять футболистами.

ТЗ для проекта было взято от сюда: https://docs.google.com/document/d/15T_msy1xkvhc8uoDUwuEsqGrUi3OdAT2B2UfDJo92PQ

## 📖 О проекте

Данное приложение разработано для создания и управления каталогом футболистов. Оно состоит из двух страниц с функциями добавления и отображения информации о футболистах и командах. 

## 🚀 Технологии

- Язык разработки backend: C#
- База данных: PostgreSQL
- Фреймворки: ASP.NET Core 8, Swagger, SignalR, EntityFramework
- балансировщик: nginx-balancer
- Frontend: React, typescript
- Деплой: YandexCloud, serverless containers

## 📄 Функционал

### 1. Добавление/редактирования футболистов

- Форма для добавления/редактирования футболистов содержит следующие поля:
  - Имя
  - Фамилия
  - Пол
  - Дата рождения
  - Название команды (пользователь может выбрать из уже добавленных или создать новую)
  - Страна (выбор из фиксированного списка: Россия, США, Италия)

### 2. Список футболистов

- Вторая страница отображает актуальный список футболистов.
- Рядом с каждым футболистом присутствует кнопка для редактирования данных.
- Реализация обновления списка в реальном времени с помощью SignalR или аналогичных технологий.

## 💻 Установка и запуск локальной версии без балансивщика

1. Клонируйте репозиторий:
   
   git clone https://github.com/lehakurshev/FootballPlayersCatalog.git

2. Запустите приложение:
   
   docker-compose up --build
   

## 🌐 Использование

- Перейдите по адресу http://localhost:3000
- Добавляйте и редактируйте футболистов.

## 🔥 Деплой в yandex cloud

ТЗ для деплоя было взято от сюда: https://docs.google.com/document/d/1t4LGu5Ff9etBvZDBRZ8E4GKV53-_69lnhYU6x4HtQnQ

### 1. Backend API

- есть два контейнера backend-1, backend-2 - у них есть swagger - можно потыкать
- https://bbau6qeh21r4lsravd9a.containers.yandexcloud.net/ 
- https://bbal3n8kt8j6thg4bvo8.containers.yandexcloud.net/

### 2. Nginx Balancer

- есть контейнер nginx-balancer - он служит балансировщиком между backend-ами
- на прямую балансировать бэкенды не получилось - пришльсь сначало проксировать (тоже через NGINX) - а потом балансировать на локальмо хосте и разныз портах
- https://bbao61jrdalru8mtpu9b.containers.yandexcloud.net/
- если пообновлять страницу можно заметить что берсии быка меняются

### 3. Frontend

- есть контейнер frontend
- https://bba11jbs3b89hs157v8m.containers.yandexcloud.net/
- yandex cloud почему-то не пропускает сокеты - SignalR не работает

### 3. DB

- обычный postgresql кластер
- (в планах запихнуть и его в контейнер чтоб вообще почти ничего не платить)
- парольи имя пользователя для DB лежат в yandex cloud Lockbox/Секреты

### 3. Скрипты для деплоя

- в папке YandexCloudDeployment лежат скрипты для деплоя
-   перед запуском скриптов нужно выполнить `yc init` или ./init.sh
-   ./backend.sh - билдит образ и пушит в Container Registry - Ревизиии для контейнеров backend-1 backend-2 нужно делать самостоятельно через Консоль управления (CLI не позволяет работать с переменными окружения и секретами)
-   ./frontend.sh - аналогично билдит образ и пушит в Container Registry
-   ./nginx-balancer.sh - билдит образ и пушит в Container Registry и создает ревизию (в nginx balancer не получилось запихнуть ENV)
-   ./deploy.sh - поочередно запускает ./init.sh -> ./backend.sh -> ./frontend.sh -> ./nginx-balancer.sh