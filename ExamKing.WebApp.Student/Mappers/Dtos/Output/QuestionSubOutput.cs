namespace ExamKing.WebApp.Student
{
    public class QuestionSubOutput
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionType { get; set; }
        
        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionId { get; set; }
        
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
    }
}