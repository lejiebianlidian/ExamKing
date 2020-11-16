﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using ExamKing.Core.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 系别DTO
    /// </summary>
    public class DeptDto
    {
        /// <summary>
        /// 系别Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系别名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }
        
    }
    
    public class DeptClassesDto : DeptDto
    {
        
        /// <summary>
        /// 关联班级
        /// </summary>
        public List<ClassesDto> Classes { get; set; }
    }

}