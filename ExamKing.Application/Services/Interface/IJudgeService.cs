using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 是非题服务接口
    /// </summary>
    public interface IJudgeService
    {
        /// <summary>
        /// 是非题创建
        /// </summary>
        /// <param name="JudgeDto"></param>
        /// <returns></returns>
        public Task<JudgeDto> CreateJudge(JudgeDto JudgeDto);

        /// <summary>
        /// 是非题更新
        /// </summary>
        /// <param name="JudgeDto"></param>
        /// <returns></returns>
        public Task<JudgeDto> UpdateJudge(JudgeDto JudgeDto);

        /// <summary>
        /// 是非题删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteJudge(int id);

        /// <summary>
        /// 根据教师查询是非题分页
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<JudgeDto>> FindJudgeAllByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据id查询是非题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<JudgeDto> FindJudgeById(int id);
    }

}