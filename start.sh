#!/bin/bash

# 项目名称
export COMPOSE_PROJECT_NAME="ek_server"

# 获取短版本号
GITCOMMITHASH=`git rev-parse --short HEAD`
export GITHASH=${GITCOMMITHASH}

echo "=======项目名称:${COMPOSE_PROJECT_NAME}  版本号：${GITHASH}========"

echo "======== 正在停止docer-compose ========"
services=("student_server" "admin_server" "teacher_server")
for i in ${services[@]}  
do  
docker ps | grep "${i}" | awk '{print $1}' | xargs docker stop && docker images | grep "${i}" | awk '{print $1":"$2}' | xargs docker rmi -f
done

echo "======== 正在构建docer-compose ========"
docker-compose up --build -d

echo "======================================"
echo "==== 完成 docker-compose 构建与运行 ===="
echo "======================================"