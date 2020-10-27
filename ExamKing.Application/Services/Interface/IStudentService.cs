using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using Mapster;
using System.Threading.Tasks;

namespace ExamKing.Application.Services
{
    public interface IStudentService
    {
        /// <summary>
        /// 学生登录
        /// </summary>
        /// <returns></returns>
        public Task<TbStudent> Login(string studentNo, string password);

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public Task<TbStudent> Register(StudentDto studentDto);
    }
}
