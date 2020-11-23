using System.Collections.Generic;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 学生服务
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 分页查询班级
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<StudentDto>> FindStudentAllByPage(int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 学生登录
        /// </summary>
        /// <returns></returns>
        public Task<StudentDto> LoginStudent(string studentNo, string password);

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public Task<StudentDto> RegisterStudent(StudentDto studentDto);

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public Task<StudentDto> UpdateStudent(StudentDto studentDto);

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="stuNo"></param>
        /// <param name="idCard"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public Task<StudentDto> ForgetPass(string stuNo, string idCard, string newPass);

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteStudent(int id);
        
        /// <summary>
        /// 查找学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<StudentDto> FindStudentById(int id);
        
        
    }
}
