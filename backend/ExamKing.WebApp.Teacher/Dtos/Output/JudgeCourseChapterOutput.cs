namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 是非题课程章节信息
    /// </summary>
    public class JudgeCourseChapterOutput : JudgeOutput
    {
        /// <summary>
        /// 课程信息
        /// </summary>
        public CourseSubOutput Course { get; set; }

        /// <summary>
        /// 章节信息
        /// </summary>
        public ChapterSubOutput Chapter { get; set; }
    }
}