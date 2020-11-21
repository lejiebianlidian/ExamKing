using Fur;
using Fur.Authorization;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// JWT 授权自定义处理程序
    /// </summary>
    /// <remarks>
    /// 可以在这里自定义自己的权限
    /// </remarks>
    public class JwtAuthorizationHandler : AppAuthorizeHandler
    {
        public override bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = httpContext.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(bearerToken)) return false;

            // 获取 token
            var accessToken = bearerToken[7..];
            
            // 验证token
            var (IsValid, Token) = JWTEncryption.Validate(accessToken, App.GetOptions<JWTSettingsOptions>());
            
            if (!IsValid) return false;

            // 检查权限
            return CheckAuthorzie(httpContext, Token);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CheckAuthorzie(DefaultHttpContext httpContext, JsonWebToken token)
        {
            // // 获取权限特性
            // var securityDefineAttribute = httpContext.GetEndpoint().Metadata.GetMetadata<SecurityDefineAttribute>();
            // if (securityDefineAttribute == null) return true;
            //
            // // 获取用户Id
            var userId = token.GetPayloadValue<int>("UserId");

            // ====================== 后续这里应该缓存起来（目前只是演示） ======================

            // // 查询用户拥有的权限
            // var securities = Db.GetRepository<User>()
            //     .Include(u => u.Roles)
            //         .ThenInclude(u => u.Securities)
            //     .Where(u => u.Id == userId)
            //     .SelectMany(u => u.Roles
            //         .SelectMany(u => u.Securities))
            //     .Select(u => u.UniqueName);
            //
            // if (!securities.Contains(securityDefineAttribute.ResourceId)) return false;
            if (userId > 0) return true;
            return false;
        }
    }
}