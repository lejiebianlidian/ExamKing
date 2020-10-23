using Fur.DynamicApiController;

namespace ExamKing.Application.Admin.v1
{
    [DynamicApiController]
    [ApiDescriptionSettings("Admin@v1", Module = "v1/admin")]
    public class ControllerBase : IDynamicApiController
    {
        public ControllerBase()
        {
        }
    }
}
