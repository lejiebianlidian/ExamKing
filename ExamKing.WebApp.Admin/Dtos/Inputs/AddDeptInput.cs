using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 新增系别输入
    /// </summary>
    public class AddDeptInput
    {
        /// <summary>
        ///系别名称
        /// </summary>
        [Required(ErrorMessage = "请输入系别名称")]
        public string DeptName { get; set; }
    }
}
