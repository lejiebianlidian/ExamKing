using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员登录输入
    /// </summary>
    public class LoginAdminInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}