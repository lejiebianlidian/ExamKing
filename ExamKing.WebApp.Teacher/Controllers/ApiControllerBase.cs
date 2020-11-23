using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using ExamKing.WebApp.Teacher;
using Fur;
using Fur.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 管理员API模块控制器基类
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings(Module = "v1")]
    public class ApiControllerBase : IDynamicApiController
    {
        /// <summary>
        /// HTTP 上下文
        /// </summary>
        protected IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected ApiControllerBase()
        {
            // 上下文
            _httpContextAccessor = App.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 获取教师实体
        /// </summary>
        /// <returns></returns>
        protected async Task<TeacherDto> GetTeacher()
        {
            var authorizationManager = App.GetService<IAuthorizationManager>();
            var id = authorizationManager.GetUserId<int>();
            var teacherService = App.GetService<ITeacherService>();
            return await teacherService.FindTeacherById(id);
        }
    }
}