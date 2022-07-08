# build
FROM node:16.14.0-alpine AS builder
# 切换编译目录
WORKDIR /build
COPY ./frontend/teacher ./
# 编译项目
RUN npm config set registry https://registry.npmmirror.com/ \
  && npm install \
  && npm run build:prod

# web
FROM nginx:alpine
# 镜像信息
LABEL author="pig224" email="xiaomo_0258@icloud.com"
# 时区
ENV TZ=Asia/Shanghai

COPY ./docker/frontend/teacher/nginx/logs /var/log/nginx
COPY ./docker/frontend/teacher/nginx/vhost /etc/nginx/conf.d/
COPY ./docker/frontend/teacher/nginx/nginx.conf /etc/nginx/nginx.conf

# 拷贝项目
COPY --from=builder /build/dist/ /var/www/html