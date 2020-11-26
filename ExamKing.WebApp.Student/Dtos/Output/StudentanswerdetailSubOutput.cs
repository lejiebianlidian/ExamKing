namespace ExamKing.WebApp.Student
{
    public class StudentanswerdetailSubOutput
    {
        /// <summary>
        /// 答题详情Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionType { get; set; }
        
        /// <summary>
        /// 是否做题正确
        /// </summary>
        public string Isright { get; set; }
    }
}