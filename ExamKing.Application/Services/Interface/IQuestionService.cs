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

        /// <summary>
        /// 根据考试查询学生是非题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Task<ExamquestionDto> FindJudgeAndAnswerByExamAndStudent(int id, int questionId, int studentId);

        /// <summary>
        /// 根据考试查询学生选择题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Task<ExamquestionDto> FindSelectAndAnswerByExamAndStudent(int id, int questionId, int studentId);

        /// <summary>
        /// 根据考试查询学生单选题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Task<ExamquestionDto> FindSingleAndAnswerByExamAndStudent(int id, int questionId, int studentId);
    }
}