using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 教师登录输出
    /// </summary>
    public class TeacherLoginOutput
    {
        /// <summary>
        /// 教师ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string AccessToken { get; set; }
    }
}