using System;
using System.Text.Json.Serialization;


namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 选择题DTO
    /// </summary>
    public class SelectDto
    {
        /// <summary>
        /// 选择题Id
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
        /// 是否单选
        /// </summary>
        public string IsSingle { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课程章节ID
        /// </summary>
        public int ChapterId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 选项A
        /// </summary>
        public string OptionA { get; set; }
        /// <summary>
        /// 选项B
        /// </summary>
        public string OptionB { get; set; }
        /// <summary>
        /// 选项C
        /// </summary>
        public string OptionC { get; set; }
        /// <summary>
        /// 选项D
        /// </summary>
        public string OptionD { get; set; }
        /// <summary>
        /// 解题思路
        /// </summary>
        public string Ideas { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTimeOffset CreateTime { get; set; }
        
        /// <summary>
        /// 课程
        /// </summary>
        public CourseDto Course { get; set; }
        
        /// <summary>
        /// 课程章节
        /// </summary>
        public ChapterDto Chapter { get; set; }
    }
}