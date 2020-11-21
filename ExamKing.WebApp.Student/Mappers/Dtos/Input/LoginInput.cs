using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 登录登录输入
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "请输入学号"), MinLength(3, ErrorMessage =
            "请输入正确的学号")]
        public string StudentNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码"), MinLength(6, ErrorMessage = "请输入正确的密码")]
        public string Password { get; set; }
    }
}
