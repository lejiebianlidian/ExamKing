# Run Services
FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0-alpine
# 构建版本字符
ARG gitcommithash
# 镜像信息
LABEL author="pig224" email="xiaomo_0258@icloud.com" version="${gitcommithash}"
# 切换运行版本目录
WORKDIR /home/publish/student
# 运行服务
ENTRYPOINT ["dotnet", "ExamKing.WebApp.Student.dll"]