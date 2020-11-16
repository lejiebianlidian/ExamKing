﻿using System;
using System.Text.Json.Serialization;
using ExamKing.Core.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 班级 DTO
    /// </summary>
    public class ClassesDto
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassesName { get; set; }

        ///// <summary>
        ///// 系别Id
        ///// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DeptId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }

        /// <summary>
        /// 系别
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DeptDto Dept { get; set; }
    }

}
