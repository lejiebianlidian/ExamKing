using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 考试成绩服务接口
    /// </summary>
    public interface IStuscoreService
    {
        
        /// <summary>
        /// 查询学生全部考试成绩分页接口
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<StuscoreDto>> FindScoreAllByStudentAndPage(int studentId, int pageIndex = 1,
            int pageSize = 10);

        /// <summary>
        /// 查询成绩详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<StuscoreDto> FindScoreById(int id);
        
        /// <summary>
        /// 查询学生最新一条成绩接口
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<StuscoreDto> FindScoreTodayByStudent(int studentId);
    }
}