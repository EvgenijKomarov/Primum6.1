FROM node:22-alpine AS builder
WORKDIR /app

ARG VITE_TELEGRAM_URL
ARG VITE_MAX_URL
ARG VITE_TELEGRAM_URL

ENV VITE_TELEGRAM_URL=$VITE_TELEGRAM_URL
ENV VITE_MAX_URL=$VITE_MAX_URL
ENV VITE_VK_URL=$VITE_VK_URL

COPY package*.json ./
RUN npm ci

COPY . .

RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
