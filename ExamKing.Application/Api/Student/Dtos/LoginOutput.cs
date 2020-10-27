using System.ComponentModel.DataAnnotations;

namespace ExamKing.Application.Api.Student
{
    /// <summary>
    /// 登录输出参数
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
