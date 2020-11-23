using System.Security.Claims;
using ExamKing.WebApp.Admin;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager, ITransient
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public bool CheckSecurity(string resourceId)
        {
            // ========= 暂时未计划RBAC 以下是鉴权代码 ===========

            return true;
        }
    }
}