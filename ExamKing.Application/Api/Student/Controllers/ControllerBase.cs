using Fur.DynamicApiController;

namespace ExamKing.Application.Api.Student
{
    [DynamicApiController]
    [ApiDescriptionSettings("Student@v1", Module = "v1/student")]
    public class ControllerBase : IDynamicApiController
    {
        public ControllerBase()
        {
        }
    }
}
