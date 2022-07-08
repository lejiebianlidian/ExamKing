using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 答题服务接口
    /// </summary>
    public interface IStuanswerdetailService
    {
        /// <summary>
        /// 获取学生全部错题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<int> GetWrongAnswerByStudent(int studentId);

        /// <summary>
        /// 获取学生今日错题数
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<int> GetWrongAnswerTodayByStudent(int studentId);

        /// <summary>
        /// 学生答题
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <param name="examQuestionId">考试问题Id</param>
        /// <param name="answer">回答内容</param>
        /// <returns></returns>
        public Task<StuanswerdetailDto> AnswerQuestionByStudent(int studentId, int examQuestionId, string[] answer);

        /// <summary>
        /// 根据学生查询考试错题集列表
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindWrongByStudentAndPage(int studentId, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据学生查询今日考试错题集列表
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindWrongTodayByStudentAndPage(int studentId, int pageIndex = 1, int pageSize = 10);

        
        /// <summary>
        /// 根据学生查询错题本详情
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<ExamDto> FindWrongInfoByExamAndStudent(int examId, int studentId);

        /// <summary>
        /// 学生根据考试查询单选题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindWrongSinglesByExamAndStudentAndPage(
            int examId, 
            int studentId,
            int pageIndex = 1, 
            int pageSize = 10);
        
        /// <summary>
        /// 学生根据考试查询多选题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindWrongSelectsByExamAndStudentAndPage(
            int examId, 
            int studentId,
            int pageIndex = 1, 
            int pageSize = 10);
        
        /// <summary>
        /// 学生根据考试查询是非题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamquestionDto>> FindWrongJudgesByExamAndStudentAndPage(
            int examId, 
            int studentId,
            int pageIndex = 1, 
            int pageSize = 10);
    }
}