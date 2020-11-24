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
        /// 获取学生错题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        public Task<int> GetWrongAnswerCountByStudent(int studentId, int examId, string questionType="all");

        /// <summary>
        /// 获取学生对题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        public Task<int> GetSuccessAnswerCountByStudent(int studentId, int examId, string questionType="all");

        /// <summary>
        /// 学生答题
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <param name="examQuestionId">考试问题Id</param>
        /// <param name="answer">回答内容</param>
        /// <returns></returns>
        public Task<StuanswerdetailDto> AnswerQuestionByStudent(int studentId, int examQuestionId, string[] answer);
    }
}