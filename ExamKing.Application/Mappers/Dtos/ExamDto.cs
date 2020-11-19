using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ExamKing.Core.Entites;

using Fur.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 试卷DTO
    /// </summary>
    public class ExamDto
    {
        private string _isEnable = "0";

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
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// 考试时长
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [DefaultValue("0")]
        public string IsEnable
        {
            get => _isEnable;
            set => _isEnable = value;
        }

        /// <summary>
        /// 结束状态
        /// </summary>
        [DefaultValue("0")]
        public string IsFinish
        {
            get => _isEnable;
            set => _isEnable = value;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// 试卷总分数
        /// </summary>
        public int ExamScore { get; set; }

        /// <summary>
        /// 是非题分数
        /// </summary>
        public int JudgeScore { get; set; }

        /// <summary>
        /// 单选题分数
        /// </summary>
        public int SingleScore { get; set; }

        /// <summary>
        /// 多选题分数
        /// </summary>
        public int SelectScore { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public CourseDto Course { get; set; }

        /// <summary>
        /// 试卷题目
        /// </summary>
        public ICollection<ExamquestionDto> Examquestions { get; set; }
        
        
    }
}