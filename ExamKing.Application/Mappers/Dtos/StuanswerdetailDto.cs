using System;
using System.Collections.Generic;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 考试答题详情Dto
    /// </summary>
    public class StuanswerdetailDto
    {
        /// <summary>
        /// 答题详情Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生Id
        /// </summary>
        public int StuId { get; set; }
        /// <summary>
        /// 考试Id
        /// </summary>
        public int ExamId { get; set; }
        /// <summary>
        /// 题目id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionType { get; set; }
        /// <summary>
        /// 学生答题内容
        /// </summary>
        public string Stuanswer { get; set; }
        /// <summary>
        /// 正确答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 是否做题正确
        /// </summary>
        public string Isright { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

    }
}