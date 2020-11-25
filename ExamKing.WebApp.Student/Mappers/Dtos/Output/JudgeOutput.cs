using System;
using System.Text.Json.Serialization;


namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 是非题信息
    /// </summary>
    public class JudgeOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 章节ID
        /// </summary>
        public int ChapterId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public int TeacherId { get; set; }
        
    }
}