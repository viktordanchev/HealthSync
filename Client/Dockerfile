# Използваме Node alpine за по-малък размер на образа
FROM node:alpine AS build

# Създаваме работната директория
WORKDIR /app

# Копираме само необходимите файлове от package.json в контейнера
COPY Client/package.json ./    # Без да използваме * за всички пакети, които са необходими
COPY Client/package-lock.json ./  # Ако има този файл

# Инсталираме зависимостите
RUN npm install --production

# Копираме целия клиентски код в контейнера
COPY Client/ ./  # Копираме целия код от Client в контейнера

# Създаваме production build на React приложението
RUN npm run build

# Проверяваме съдържанието на /app, за да видим дали има build директория
RUN ls -la /app

# Започваме да обслужваме приложението чрез static файловете
FROM nginx:alpine

# Копираме статичния build от предишния етап в nginx директорията
COPY --from=build /app/build /usr/share/nginx/html

# Излагаме порт 80 за Nginx
EXPOSE 80

# Стартираме Nginx като процес
CMD ["nginx", "-g", "daemon off;"]