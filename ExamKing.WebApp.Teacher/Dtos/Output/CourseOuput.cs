using System.Collections.Generic;
using ExamKing.Core.JsonConverters;
using Newtonsoft.Json;

namespace ExamKing.WebApp.Teacher
{
    public class CourseOutput
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        
        /// <summary>
        /// 系别Id
        /// </summary>
        public int DeptId { get; set; }
        
        /// <summary>
        /// 教师Id
        /// </summary>
        public int TeacherId { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }
        
    }

}