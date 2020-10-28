using Fur.Authorization;
using Fur.DynamicApiController;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生API模块控制器基类
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings(Module = "v1/student")]
    public class ApiControllerBase : IDynamicApiController
    {
    }
}