FROM node:alpine AS build

WORKDIR /app

COPY ./Client/package*.json ./

RUN npm install

COPY . .

EXPOSE 5173

CMD ["npm", "run", "dev"]