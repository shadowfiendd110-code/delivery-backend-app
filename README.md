# 🚚 Система приёма заказов на доставку

Тестовое приложение для создания и просмотра заказов на доставку. Реализовано на **ASP.NET Core 8 Web API** и **React + Vite**. Данные хранятся в **SQLite**. Приложение полностью контейнеризировано с помощью Docker.

## 📋 Функциональность

- **Форма создания заказа** (все поля обязательны):
  - Город и адрес отправителя
  - Город и адрес получателя
  - Вес груза (кг)
  - Дата забора груза
- Автоматическая генерация уникального номера заказа
- **Список всех заказов** с краткой информацией
- **Детальный просмотр** заказа в режиме "только чтение" по клику на строку

## 🛠 Технологии

| Компонент | Технологии |
|-----------|------------|
| **Backend** | .NET 8, Entity Framework Core, SQLite, ASP.NET Core Web API |
| **Frontend** | React 18, Vite, Axios |
| **Контейнеризация** | Docker, Docker Compose |

## 📁 Структура проекта
delivery-backend-app/
├── backend/ # ASP.NET Core бэкенд
│ ├── Controllers/
│ ├── Services/
│ ├── Repositories/
│ ├── Data/
│ ├── Dockerfile
│ └── ...
├── frontend/ # React фронтенд
│ ├── src/
│ ├── public/
│ ├── Dockerfile
│ └── ...
├── docker-compose.yml # Оркестрация контейнеров
└── README.md


## 🚀 Быстрый старт (Docker)

Этот способ не требует установки .NET SDK или Node.js.

### Предварительные требования

- Установленный **Docker Desktop** (должен быть запущен)
- **Git** (для клонирования)

### Запуск

1. **Клонируйте репозиторий:**
   
   git clone https://github.com/shadowfiendd110-code/delivery-backend-app.git
   cd delivery-backend-app

2. Запустите контейнеры:
  
  docker-compose up -d

3. Приложение будет доступно по адресам:

  Сервис	              Адрес
  Frontend (React)	    http://localhost:8081
  Backend API (Swagger)	http://localhost:8080/swagger

4.Остановка приложения:

docker-compose down

Запуск бэкенда

cd backend
dotnet restore
dotnet run --urls "http://localhost:8080"
Swagger будет доступен по адресу: http://localhost:8080/swagger

Запуск фронтенда

cd frontend
npm install
npm run dev

Приложение будет доступно по адресу: http://localhost:5173

🐳 Сборка Docker образов вручную
Если вы хотите собрать образы без Docker Compose:

Бэкенд

cd backend
docker build -t delivery-backend .
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development --name delivery-backend delivery-backend

Фронтенд

cd frontend
docker build -t delivery-frontend .
docker run -d -p 8081:80 --name delivery-frontend delivery-frontend

📝 Примечания
1) База данных SQLite создаётся автоматически внутри контейнера бэкенда
2) Номер заказа генерируется автоматически в формате ORD-{ticks}-{random}
3) При первом запуске через Docker Compose может потребоваться время на скачивание образов
