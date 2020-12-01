using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 自动组卷输入
    /// </summary>
    public class AddAutoExamInput
    {

        /// <summary>
        /// 试卷名称
        /// </summary>
        [Required(ErrorMessage = "请输入试卷名称")]
        public string ExamName { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属课程")]
        public int CourseId { get; set; }

        /// <summary>
        /// 教师ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属教师")]
        public int TeacherId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请选择开始考试时间")]
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请选择结束考试试卷")]
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// 考试时长
        /// </summary>
        [Required(ErrorMessage = "请选择结束考试试卷")]
        public int Duration { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [Required(ErrorMessage = "请选择启用状态")]
        public string IsEnable { get; set; }

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

    }
}