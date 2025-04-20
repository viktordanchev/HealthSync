FROM node:20-alpine AS build

WORKDIR /app

COPY Client/package*.json ./
RUN npm install

COPY Client/ ./
RUN npm run build

FROM node:20-alpine AS production

WORKDIR /app

RUN npm install -g serve

COPY --from=build /app/dist ./dist

EXPOSE 5173

CMD ["serve", "-s", "dist", "-l", "5173"]