using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 题库服务接口
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// 根据考试查询是非题分页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindJudgeByExamAndPage(int id, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据考试查询选择题分页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindSelectByExamAndPage(int id, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据考试查询单选题分页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindSingleByExamAndPage(int id, int pageIndex = 1, int pageSize = 10);
    
    }
}