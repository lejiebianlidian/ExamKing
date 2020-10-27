using System.ComponentModel.DataAnnotations;

namespace ExamKing.Application.Api.Student
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Required, MinLength(3)]
        public string StudentNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
