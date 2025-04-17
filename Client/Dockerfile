# Build stage
FROM node:18-alpine AS build

WORKDIR /app

# Копираме package.json и package-lock.json
COPY package*.json ./

# Инсталираме зависимостите
RUN npm install

# Debug: Покажи съдържанието на директорията
RUN ls -la /app

# Копираме всички останали файлове в контейнера
COPY . .

# Създаваме билд
RUN npm run build

# Production stage
FROM nginx:alpine

# Копираме билднатото React приложение в nginx директорията
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]