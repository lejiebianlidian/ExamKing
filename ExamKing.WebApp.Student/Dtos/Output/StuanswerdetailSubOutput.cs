namespace ExamKing.WebApp.Student
{
    public class StuanswerdetailSubOutput
    {
        /// <summary>
        /// 答题详情Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 学生答题内容
        /// </summary>
        public string Stuanswer { get; set; }
        
        /// <summary>
        /// 正确答案
        /// </summary>
        public string Answer { get; set; }
    }
}