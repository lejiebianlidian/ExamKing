using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生登录输出
    /// </summary>
    public class LoginOutput
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string AccessToken { get; set; }

    }
}
