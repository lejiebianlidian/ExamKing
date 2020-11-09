#nullable enable
using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 学生修改信息输入
    /// </summary>
    public class EditStudentInput
    {

        /// <summary>
        /// 学生ID
        /// </summary>
        [Required(ErrorMessage = "请选择学生")]
        public int Id { get; set; }
        
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StuName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
        
    }
}