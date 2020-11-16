using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 更新系别输入
    /// </summary>
    public class EditDeptInput
    {
        /// <summary>
        /// 系别Id
        /// </summary>
        [Required(ErrorMessage = "系别Id不能为空")]
        public int Id { get; set; }

        /// <summary>
        /// 系别名称
        /// </summary>
        [Required(ErrorMessage = "系别名称不能为空")]
        public string DeptName { get; set; }
    }
}