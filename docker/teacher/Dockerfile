# Restore And Publish
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0-alpine AS builder
# 切换编译目录
WORKDIR /build
COPY ./ ./

# 编译项目
RUN dotnet restore \ 
  && dotnet publish "./ExamKing.WebApp.Teacher/ExamKing.WebApp.Teacher.csproj" -c release -o ./publish --no-restore

# Run Services
FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0-alpine
# 镜像信息
LABEL author="pig224" email="xiaomo_0258@icloud.com"
# 时区
RUN sed -i "s@http://dl-cdn.alpinelinux.org/@https://repo.huaweicloud.com/@g" repositories  \
  && apk --no-cache add tzdata \
  && ln -snf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime \
  && echo 'Asia/Shanghai' > /etc/timezone

# 运行项目
WORKDIR /app
COPY --from=builder /build/publish/ ./
EXPOSE 80
# 运行服务
ENTRYPOINT ["dotnet", "ExamKing.WebApp.Teacher.dll"]