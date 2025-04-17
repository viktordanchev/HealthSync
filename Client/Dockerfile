# Използваме Node alpine за по-малък размер на образа
FROM node:alpine AS build

# Създаваме работната директория
WORKDIR /app

# Копираме само файловете, необходими за инсталация на зависимостите
COPY Client/package*.json ./

# Инсталираме зависимостите
RUN npm install --production

# Копираме целия клиентски код
COPY Client/ ./

# Създаваме production build на React приложението
RUN npm run build

# Преглеждаме съдържанието на директорията /app, за да видим дали има build
RUN ls -la /app

# Започваме да обслужваме приложението чрез static файловете
FROM nginx:alpine

# Копираме статичния build от предишния етап в nginx директорията
COPY --from=build /app/build /usr/share/nginx/html

# Излагаме порт 80 за Nginx
EXPOSE 80

# Стартираме Nginx като процес
CMD ["nginx", "-g", "daemon off;"]
