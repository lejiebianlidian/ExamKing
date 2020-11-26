namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 答题输入
    /// </summary>
    public class AddExamAnswerInput
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        public int questionId { get; set; }

        /// <summary>
        /// 提交答案
        /// </summary>
        public string[] answer { get; set; }
    }
}