using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.DatabaseAccessor;
using Mapster;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 学生接口
    /// </summary>
    public class StudentController : ApiControllerBase
    {
        private readonly IStudentService _studentService;
        /// <summary>
        /// 依赖注入
        /// </summary>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 学生列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<StudentInfoDto>> GetAllDept(int pageIndex = 1, int pageSize = 10)
        {
            var stuList = await _studentService.FindStudentAllByPage(pageIndex, pageSize);
            return stuList.Adapt<PagedList<StudentInfoDto>>();
        }
        
        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="addStudentInput"></param>
        /// <returns></returns>
        public async Task<StudentInfoDto> PostCreateStudent(AddStudentInput addStudentInput)
        {
            var stu = await _studentService.RegisterStudent(addStudentInput.Adapt<StudentDto
            >());
            return stu.Adapt<StudentInfoDto>();
        }

        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="editStudentInput"></param>
        /// <returns></returns>
        public async Task UpdateEditStudent(EditStudentInput editStudentInput)
        { 
            await _studentService.UpdateStudent(editStudentInput
                .Adapt<StudentDto>());
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRemoveStudent(int id)
        {
            await _studentService.DeleteStudent(id);
        }
    }
}