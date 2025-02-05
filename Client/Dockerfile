FROM node AS build

WORKDIR /app/Client

COPY package.json .

RUN npm install

COPY . .

RUN npm run build

EXPOSE 3000

CMD [ "serve", "-s", "dist" ]