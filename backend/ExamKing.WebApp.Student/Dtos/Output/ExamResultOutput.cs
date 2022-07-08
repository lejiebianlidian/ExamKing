using System.Collections.Generic;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试结果输出
    /// </summary>
    public class ExamResultOutput : ExamOutput
    {
        /// <summary>
        /// 题目
        /// </summary>
        public List<ExamResultQuestionSubOutput> Examquestions { get; set; }
        
        /// <summary>
        /// 答题
        /// </summary>
        public List<StudentanswerdetailSubOutput> Stuanswerdetails { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public List<StuscoreSubOutput> Stuscores { get; set; }
    }

    public class ExamResultQuestionSubOutput
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