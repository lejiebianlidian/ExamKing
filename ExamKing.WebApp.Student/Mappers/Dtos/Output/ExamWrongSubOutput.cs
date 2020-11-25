using System.Collections.Generic;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 错题列表输出
    /// </summary>
    public class ExamWrongSubOutput
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string ExamName { get; set; }

        public List<ExamWrongQuestionSubOutput> Stuanswerdetails { get; set; }
    }

    public class ExamWrongQuestionSubOutput
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionType { get; set; }
    }
}