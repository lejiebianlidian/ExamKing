using System.Threading.Tasks;
using ExamKing.Application.Services;
using ExamKing.Application.Mappers;
using Mapster;
using Fur.FriendlyException;

namespace ExamKing.Application.Api.Student.Controllers
{
    /// <summary>
    /// 班级接口
    /// </summary>
    public class ClassesController : ControllerBase
    {
        private readonly IClassesService _classesService;
        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }

        [IfException(1000, ErrorMessage = "系别不存在")]
        public async Task<AddClassOutput> InsertAddClass(AddClassInput addClassInput)
        {
            var classes = await _classesService.InsertClass(addClassInput.Adapt<ClassesDto>());
            return classes.Adapt<AddClassOutput>();
        }
    }
}
