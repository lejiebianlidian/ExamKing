namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 选择题关联课程章节输出
    /// </summary>
    public class SelectCourseChapterOutput : SelectOutput
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