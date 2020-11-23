using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 试卷输出
    /// </summary>
    public class ExamOutput
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
        public string IsEnable { get; set; }

        /// <summary>
        /// 结束状态
        /// </summary>
        public string IsFinish { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
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
        /// 班级
        /// </summary>
        public List<ClassesDeptSubOutput> Classes { get; set; }
    }
}