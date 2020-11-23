using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace ExamKing.WebApp.Admin
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
        public async Task<AdminDto> GetAdmin()
        {
            var adminService = App.GetService<IManageService>();
            var admin = await adminService.FindAdminById(
                int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId")));
            return admin;
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