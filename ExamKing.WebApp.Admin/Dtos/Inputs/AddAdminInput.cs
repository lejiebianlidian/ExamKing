using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员注册输入
    /// </summary>
    public class RegisterAdminInput
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