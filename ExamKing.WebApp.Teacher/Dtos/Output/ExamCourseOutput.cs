namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 试卷课程输出
    /// </summary>
    public class ExamCourseOutput : ExamOutput
    {
        /// <summary>
        /// 课程
        /// </summary>
        public CourseSubOutput Course { get; set; }
    }
}