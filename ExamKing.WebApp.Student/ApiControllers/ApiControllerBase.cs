using Fur;
using Fur.Authorization;
using Fur.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
        /// 获取学生Id
        /// </summary>
        /// <returns></returns>
        protected int getUserId()
        {
            var authorizationManager = App.GetService<IAuthorizationManager>();
            return authorizationManager.GetUserId<int>();
        }
    }
}