using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    public class EditTeacherInput
    {
        private string _password = "";

        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "请选择教师")]
        public int Id { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
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
        /// 密码
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        /// <summary>
        /// 系别Id
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
    }
}