using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Fur.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 课程 Dto
    /// </summary>
    public class CourseDto
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// 教师
        /// </summary>
        public TeacherDto Teacher { get; set; }
        
        /// <summary>
        /// 班级
        /// </summary>
        public List<ClassesDto> Classes { get; set; }
    }
}