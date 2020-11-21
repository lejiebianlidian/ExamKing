using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 教师登录输入
    /// </summary>
    public class TeacherLoginInput
    {
        /// <summary>
        /// 工号 
        /// </summary>
        [Required(ErrorMessage = "请输入工号")]
        public string TeacherNo { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}