using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试题目和答题信息输出
    /// </summary>
    public class ExamQuestionAnswerOutput : ExamquestionSelectOutput
    {
        /// <summary>
        /// 答题详情
        /// </summary>
        public StuanswerdetailDto Stuanswerdetail { get; set; }
    }
}