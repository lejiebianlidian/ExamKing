using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    public class EditAdminInput
    {
        private string _password = "";
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "请选择管理员")]
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入管理员账号")]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}