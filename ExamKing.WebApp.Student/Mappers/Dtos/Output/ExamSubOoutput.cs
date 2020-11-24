using System;

namespace ExamKing.WebApp.Student
{
    public class ExamSubOoutput
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
        /// 开始时间
        /// </summary>
        public DateTimeOffset StartTime { get; set; }
        
        /// <summary>
        /// 考试时长
        /// </summary>
        public int Duration { get; set; }
    }
}