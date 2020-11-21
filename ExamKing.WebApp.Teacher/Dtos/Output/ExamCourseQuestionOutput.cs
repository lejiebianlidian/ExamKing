using System.Collections.Generic;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 试卷课程题目输出
    /// </summary>
    public class ExamCourseQuestionOutput : ExamOutput
    {
        /// <summary>
        /// 课程
        /// </summary>
        public CourseSubOutput Course { get; set; }

        /// <summary>
        /// 试卷题目
        /// </summary>
        public List<ExamquestionOutput> Examquestions { get; set; }
        
    }
}