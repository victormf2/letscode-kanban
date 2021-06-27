FROM node:14.17-alpine AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:1-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/letscode-kanban /usr/share/nginx/html