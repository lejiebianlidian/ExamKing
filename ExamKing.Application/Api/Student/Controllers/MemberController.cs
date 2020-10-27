using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.FriendlyException;
using Mapster;

namespace ExamKing.Application.Api.Student
{
    /// <summary>
    /// 学生接口
    /// </summary>
    public class MemberController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public MemberController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 学生登录接口
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        public async Task<LoginOutput> PostLogin(LoginInput loginInput)
        {
            var student = await _studentService.Login(loginInput.StudentNo, loginInput.Password);
            return student.Adapt<LoginOutput>();
        }

        /// <summary>
        /// 学生注册接口
        /// </summary>
        /// <param name="registerInput"></param>
        /// <returns></returns>
        public async Task<ResgisterOutput> PostRegister(ResgisterInput resgisterInput)
        {
            var student = await _studentService.Register(resgisterInput.Adapt<StudentDto>());
            return student.Adapt<ResgisterOutput>();
        }

    }
}