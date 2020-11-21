using System;
using System.Text.Json.Serialization;


namespace ExamKing.WebApp.Teacher
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
        /// 答案
        /// </summary>
        public string Answer { get; set; }
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
        /// <summary>
        /// 解题思路
        /// </summary>
        public string Ideas { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }
        
    }
}