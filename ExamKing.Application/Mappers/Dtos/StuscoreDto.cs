using System;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 学生成绩DTO
    /// </summary>
    public class StuscoreDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StuId { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExamId { get; set; }
        /// <summary>
        /// 考试成绩
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// 考试
        /// </summary>
        public ExamDto Exam { get; set; }
    }
}