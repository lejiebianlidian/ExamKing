using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员登录输出
    /// </summary>
    public class LoginAdminOutput
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string AccessToken { get; set; }
    }
}