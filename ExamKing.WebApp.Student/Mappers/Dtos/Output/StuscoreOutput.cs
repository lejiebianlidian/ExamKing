using System;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试成绩输出
    /// </summary>
    public class StuscoreOutput
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StuId { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExamId { get; set; }
        /// <summary>
        /// 考试成绩
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }
    }
}