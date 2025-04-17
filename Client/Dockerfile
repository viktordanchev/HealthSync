FROM node:alpine AS build

WORKDIR /app

COPY Client/package.json ./
COPY Client/package-lock.json ./
RUN npm install

COPY Client/ ./

RUN npm run build

RUN ls -la /app/dist

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
