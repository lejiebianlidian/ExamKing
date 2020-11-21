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
    
        /// <summary>
        /// 根据考试查询全部是非题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<ExamquestionDto>> FindJudgeByExam(int id);

        /// <summary>
        /// 根据考试查询全部选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<ExamquestionDto>> FindSelectByExam(int id);

        /// <summary>
        /// 根据考试查询全部单选题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<ExamquestionDto>> FindSingleByExam(int id);

    }
}