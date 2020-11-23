using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Furion;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生API模块控制器基类
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings(Module = "v1")]
    public class ApiControllerBase : IDynamicApiController
    {

        /// <summary>
        /// HTTP 上下文
        /// </summary>
        protected readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected ApiControllerBase()
        {
            // 上下文
            _httpContextAccessor = App.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 获取学生实体
        /// </summary>
        /// <returns></returns>
        protected async Task<StudentDto> GetStudent()
        {
            var authorizationManager = App.GetService<IAuthorizationManager>();
            var studentId = authorizationManager.GetUserId();
            var studentService = App.GetService<IStudentService>();
            return await studentService.FindStudentById(studentId);
        }
    }
}