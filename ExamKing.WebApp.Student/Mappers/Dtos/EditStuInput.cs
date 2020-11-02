#nullable enable
using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生修改信息输入
    /// </summary>
    public class EditStuInput
    {
        
        /// <summary>
        /// 学生姓名
        /// </summary>
        [Required(ErrorMessage = "请输入姓名")]
        public string StuName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        public string Telphone { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required(ErrorMessage = "请输入身份证号码")]
        public string IdCard { get; set; }
        
    }
}