using System.Text.Json.Serialization;
using ExamKing.Core.JsonConverters;

namespace ExamKing.Application.Mappers
{
    public class JudgeDto
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
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }
        
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