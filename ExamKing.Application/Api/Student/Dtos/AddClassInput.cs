using System;
namespace ExamKing.Application.Api.Student
{
    public class AddClassInput
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassesName { get; set; }

        /// <summary>
        /// 系别Id
        /// </summary>
        public int Deptld { get; set; }
    }
}
