using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using Mapster;
using System.Threading.Tasks;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 学生服务
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 学生登录
        /// </summary>
        /// <returns></returns>
        public Task<StudentDto> Login(string studentNo, string password);

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public Task<StudentDto> Register(StudentDto studentDto);

        /// <summary>
        /// 学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<StudentDto> GetInfoById(int Id);

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public Task<StudentDto> UpdateInfo(StudentDto studentDto);

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="stuNo"></param>
        /// <param name="idCard"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public Task<StudentDto> ForgetPass(string stuNo, string idCard, string newPass);
    }
}
