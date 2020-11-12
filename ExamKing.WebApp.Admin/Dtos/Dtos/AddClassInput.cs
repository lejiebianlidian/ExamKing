using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 新增班级输入
    /// </summary>
    public class AddClassInput
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        [Required(ErrorMessage = "请输入班级名称")]
        public string ClassesName { get; set; }

        /// <summary>
        /// 系别Id
        /// </summary>
        [Required(ErrorMessage = "请选择系别")]
        public int DeptId { get; set; }
    }
}
