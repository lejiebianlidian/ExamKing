using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 手动组卷输入
    /// </summary>
    public class AddExamInput
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

        // /// <summary>
        // /// 所属班级
        // /// </summary>
        // [Required(ErrorMessage = "请选择所属班级")]
        // public List<ExamClassesInput> Examclasses { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请选择开始考试时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请选择结束考试试卷")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 考试时长
        /// </summary>
        [Required(ErrorMessage = "请选择结束考试试卷")]
        public int Duration { get; set; }

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
        /// 多选题
        /// </summary>
        public List<QuestionInput> Selects { get; set; }
        
        /// <summary>
        /// 单选题
        /// </summary>
        public List<QuestionInput> Singles { get; set; }
        
        /// <summary>
        /// 是非题
        /// </summary>
        public List<QuestionInput> Judges { get; set; }
    }

    /// <summary>
    /// 试卷题目输入
    /// </summary>
    public class QuestionInput
    {
        
        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionId { get; set; }
        
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
    }

    /// <summary>
    /// 分配班级输入
    /// </summary>
    public class ExamClassesInput
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassesId { get; set; }
    }
}