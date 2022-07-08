# Restore And Publish
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
# 切换编译目录
WORKDIR /build
COPY ../backend ./

# 编译项目
RUN dotnet restore
RUN dotnet publish "./ExamKing.WebApp.Teacher/ExamKing.WebApp.Teacher.csproj" -c release -o ./publish --no-restore

# Run Services
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
# 镜像信息
LABEL author="pig224" email="xiaomo_0258@icloud.com"
# 时区
ENV TZ=Asia/Shanghai

# 运行项目
WORKDIR /app
COPY --from=build /build/publish/ ./
EXPOSE 80
EXPOSE 443
# 运行服务
ENTRYPOINT ["dotnet", "ExamKing.WebApp.Teacher.dll"]