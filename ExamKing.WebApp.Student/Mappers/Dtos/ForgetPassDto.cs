using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 找回密码输入
    /// </summary>
    public class ForgetPassDto
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "请输入学号"), MinLength(3, ErrorMessage =
             "请输入正确的学号")]
        public string StuNo { get; set; }

        /// <summary>
        /// 身份证后六位
        /// </summary>
        [Required(ErrorMessage = "请输入身份证后六位"), MinLength(6, ErrorMessage = "请输入正确的身份证后六位"), MaxLength(6, ErrorMessage = "请输入正确的身份证后六位")]
        public string IdCard { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
       [Required(ErrorMessage = "请输入新密码"), MinLength(6, ErrorMessage = "请输入正确的密码")]
        public string NewPass { get; set; }
    }
}