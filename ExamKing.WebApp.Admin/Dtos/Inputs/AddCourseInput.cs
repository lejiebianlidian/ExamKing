using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 创建课程输入
    /// </summary>
    public class AddCourseInput
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        [Required(ErrorMessage = "请输入课程名称")]
        public string CourseName { get; set; }
        
        /// <summary>
        /// 系别Id
        /// </summary>
        [Required(ErrorMessage = "请选择所属系别")]
        public int DeptId { get; set; }
        
        /// <summary>
        /// 教师Id
        /// </summary>
        [Required(ErrorMessage = "请选择所属教师")]
        public int TeacherId { get; set; }
    }
}