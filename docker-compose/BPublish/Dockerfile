# Restore And Publish
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0-alpine AS build

# 镜像信息
LABEL author="pig224" email="xiaomo_0258@icloud.com"

# 切换项目目录
WORKDIR /home/examking
COPY ./ ./

# 发布全部服务
RUN dotnet restore \ 
  && dotnet publish "./ExamKing.WebApp.Student/ExamKing.WebApp.Student.csproj" -c release -o /home/publish/student --no-restore \
  && dotnet publish "./ExamKing.WebApp.Admin/ExamKing.WebApp.Admin.csproj" -c release -o /home/publish/admin --no-restore 