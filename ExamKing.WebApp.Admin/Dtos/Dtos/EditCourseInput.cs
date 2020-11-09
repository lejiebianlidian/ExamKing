using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 更新课程输入
    /// </summary>
    public class EditCourseInput
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        [Required(ErrorMessage = "请选择课程")]
        public int Id { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 系别Id
        /// </summary>
        public int Deptld { get; set; }

        /// <summary>
        /// 教师Id
        /// </summary>
        public int Teacherld { get; set; }
    }
}