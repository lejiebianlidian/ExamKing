using System;
using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生注册输入
    /// </summary>
    public class ResgisterInput
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        [Required(ErrorMessage = "请输入姓名")]
        public string StuName { get; set; }

        /// <summary>
        /// 系别Id
        /// </summary>
        [Required(ErrorMessage = "请选择系别")]
        public int DeptId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        [Required(ErrorMessage = "请选择班级")]
        public int ClassesId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "请输入学号")]
        public string StuNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        public string Telphone { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required(ErrorMessage = "请输入身份证号码")]
        public string IdCard { get; set; }
    }
}
