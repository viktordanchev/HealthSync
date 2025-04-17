FROM node:alpine AS build

# Създаваме работната директория
WORKDIR /app

# Копираме файловете от директорията Client в контейнера
COPY Client/package.json ./
COPY Client/package-lock.json ./

# Инсталираме зависимостите
RUN npm install --production

# Копираме целия код от Client в контейнера
COPY Client/ ./  # Цялото съдържание от Client

# Създаваме build на React приложението
RUN npm run build

# Проверяваме съдържанието на /app, за да видим дали има build директория
RUN ls -la /app

# Прехвърляме build файла в nginx директорията
FROM nginx:alpine

# Копираме статичния build от предишния етап
COPY --from=build /app/build /usr/share/nginx/html

# Излагаме порт 80
EXPOSE 80

# Стартираме Nginx
CMD ["nginx", "-g", "daemon off;"]
