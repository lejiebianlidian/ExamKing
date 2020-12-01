<p></p>
<p></p>

<p align="center">
<img src="./imgs/logo.png" height="80"/>
</p>

<div align="center">

[![star](https://gitee.com/pig0224/ExamKing/badge/star.svg?theme=gvp)](https://gitee.com/pig0224/ExamKing/stargazers) 
[![fork](https://gitee.com/pig0224/ExamKing/badge/fork.svg?theme=gvp)](https://gitee.com/pig0224/ExamKing/members) 
[![GitHub stars](https://img.shields.io/github/stars/pig0224/ExamKing?logo=github)](https://github.com/pig0224/ExamKing/stargazers) 
[![GitHub forks](https://img.shields.io/github/forks/pig0224/ExamKing?logo=github)](https://github.com/pig0224/ExamKing/network) 
[![GitHub license](https://img.shields.io/github/license/pig0224/ExamKing)](https://github.com/pig0224/ExamKing/blob/master/LICENSE) 

</div>

<div align="center">

考试君 - 基于.NET 5语言的Furion框架开发在线考试系统

</div>

## 💐 架构

<p align="center">
<img src="./imgs/ExamKing-Diagram.png" height="650"/>
</p>

## 🍻 相关项目
- APP端：https://gitee.com/pig0224/ExamKing-app
- 后端：https://gitee.com/pig0224/ExamKing
- 管理员后台：https://gitee.com/pig0224/ExamKing-Admin
- 教师后台：https://gitee.com/pig0224/ExamKing-Teacher

## 🥗 环境要求

- Visual Studio 2019 16.8 +
- .NET 5 SDK +
- .Net Standard 2.1 +

> 使用Docker部署需要安装Dokcer和Docker-Compose。

## 🌭 数据迁移
> ⚠️注意：初始化数据库需进入ExamKing.Database.Core程序集配置dbsetting.json中的数据库ConnectionStrings。

```shell
cd ./ExamKing.Database.Migrations
dotnet ef database update -s "../ExamKing.WebApp.Admin"
```

## 🍿 Docker运行
```shell
./deploy.sh start
```

- 学生端接口文档：http://localhost:8071/
- 管理员端接口文档：http://localhost:8072/
- 教师端接口文档    ：http://localhost:8073/

## 🍖 预览

<p align="center">
<img src="./imgs/1.png" width="20%"/>
<img src="./imgs/2.png" width="20%"/>
<img src="./imgs/3.png" width="20%"/>
<img src="./imgs/4.png" width="20%"/>
</p>

## 🍻 贡献代码

`考试君` 遵循 `MIT` 开源协议，欢迎大家提交 `PR` 或 `Issue`。

如果要为项目做出贡献，请查看贡献指南。

## 🍚 关于作者

一个在校大学生，热爱编程、热爱代码。

## 🧆 友情链接

👉 **[Furion](https://gitee.com/monksoul/Furion)** 
👉 **[Vuejs](https://cn.vuejs.org/)** 
👉 **[Element UI](https://element.eleme.cn/)** 
👉 **[uView](https://uviewui.com/)** 
👉 **[ColorUI](https://www.color-ui.com/)** 


