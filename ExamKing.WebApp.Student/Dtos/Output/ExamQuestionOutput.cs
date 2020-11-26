using System.Collections.Generic;

namespace ExamKing.WebApp.Student
{
    public class ExamQuestionOutput : ExamOutput
    {
        /// <summary>
        /// 试卷题目
        /// </summary>
        public List<QuestionSubOutput> Examquestions { get; set; }
    }
    
}