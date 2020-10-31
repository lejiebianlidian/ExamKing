using Fur.Authorization;
using Fur.DynamicApiController;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员API模块控制器基类
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings(Module = "v1")]
    public class ApiControllerBase : IDynamicApiController
    {
    }
}