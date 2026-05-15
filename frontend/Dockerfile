# Этап 1: Сборка React приложения
FROM node:22-alpine AS build
WORKDIR /app

# Копируем файлы с зависимостями
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем весь код приложения
COPY . .

# Собираем React приложение
RUN npm run build

# Этап 2: Запуск через Nginx веб-сервер
FROM nginx:alpine

# Копируем собранное приложение из первого этапа
COPY --from=build /app/dist /usr/share/nginx/html

# Открываем порт 80
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]