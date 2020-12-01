using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// <summary>
        /// 获取教师实体
        /// </summary>
        /// <returns></returns>
        Task<TeacherDto> GetTeacher();

        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        bool CheckSecurity(string resourceId);
    }
}