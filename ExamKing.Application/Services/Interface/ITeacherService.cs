using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using Fur.DatabaseAccessor;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 教师服务
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// 分页查询教师
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<TeacherDto>> FindTeacherAllByPage(int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 创建教师
        /// </summary>
        /// <param name="teacherDto"></param>
        /// <returns></returns>
        public Task<TeacherDto> CreateTeacher(TeacherDto teacherDto);

        /// <summary>
        /// 更新教师
        /// </summary>
        /// <param name="teacherDto"></param>
        /// <returns></returns>
        public Task<TeacherDto> UpdateTeacher(TeacherDto teacherDto);

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteTeacher(int id);
        
        /// <summary>
        /// 查找教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TeacherDto> FindTeacherById(int id);
        
        /// <summary>
        /// 教师登录
        /// </summary>
        /// <param name="teacherNo"></param>
        /// <param name="passwoerd"></param>
        /// <returns></returns>
        public Task<TeacherDto> LoginTeacher(string teacherNo, string passwoerd);
        
    }
}