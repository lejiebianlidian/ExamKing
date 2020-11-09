using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 创建教师输入
    /// </summary>
    public class AddTeacherInput
    {

        /// <summary>
        /// 教师名称
        /// </summary>
        [Required(ErrorMessage = "请输入教师姓名")]
        public string TeacherName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }
        
        /// <summary>
        /// 教师工号
        /// </summary>
        [Required(ErrorMessage = "请输入登录工号")]
        public string TeacherNo { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入登录密码")]
        public string Password { get; set; }
        
        /// <summary>
        /// 系别Id
        /// </summary>
        [Required(ErrorMessage = "请选择所属系别")]
        public int Deptld { get; set; }
        
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required(ErrorMessage = "请输入身份证号码")]
        public string IdCard { get; set; }
    }
}