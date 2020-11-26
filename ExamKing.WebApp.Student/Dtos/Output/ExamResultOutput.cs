using System.Collections.Generic;
using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试结果输出
    /// </summary>
    public class ExamResultOutput : ExamOutput
    {
        /// <summary>
        /// 答题
        /// </summary>
        public List<StudentanswerdetailSubOutput> Stuanswerdetails { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public List<StuscoreSubOutput> Stuscores { get; set; }
    }
}