FROM node:alpine AS build

WORKDIR /app

COPY Client/package*.json ./
RUN npm install

COPY Client/ ./
RUN npm run build

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 5173

CMD ["nginx", "-g", "daemon off;"]