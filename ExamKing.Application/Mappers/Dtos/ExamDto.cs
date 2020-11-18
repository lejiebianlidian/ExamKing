using System.Text.Json.Serialization;
using ExamKing.Core.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 试卷DTO
    /// </summary>
    public class ExamDto
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public string ExamName { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 是非题
        /// </summary>
        public string Judges { get; set; }
        /// <summary>
        /// 单选题
        /// </summary>
        public string Singles { get; set; }
        /// <summary>
        /// 多选题
        /// </summary>
        public string Selects { get; set; }
        /// <summary>
        /// 考试时长
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string IsEnable { get; set; }
        /// <summary>
        /// 结束状态
        /// </summary>
        public string IsFinish { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }
        
        /// <summary>
        /// 课程
        /// </summary>
        public CourseDto Course { get; set; }
    }
}