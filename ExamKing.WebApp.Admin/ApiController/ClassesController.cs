using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.FriendlyException;
using Mapster;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 班级接口
    /// </summary>
    public class ClassesController : ApiControllerBase
    {
        private readonly IClassesService _classesService;

        /// <inheritdoc />
        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }
        
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="addClassInput"></param>
        /// <returns></returns>
        [IfException(1000, ErrorMessage = "系别不存在")]
        public async Task<AddClassOutput> InsertAddClass(AddClassInput addClassInput)
        {
            var classes = await _classesService.InsertClass(addClassInput.Adapt<ClassesDto>());
            return classes.Adapt<AddClassOutput>();
        }
    }
}
