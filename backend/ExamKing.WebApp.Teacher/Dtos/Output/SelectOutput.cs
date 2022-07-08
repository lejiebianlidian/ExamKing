using System;
using System.Text.Json.Serialization;
using ExamKing.Application.Mappers;


namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 选择题输出
    /// </summary>
    public class SelectOutput
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
        public DateTimeOffset CreateTime { get; set; }

    }
}