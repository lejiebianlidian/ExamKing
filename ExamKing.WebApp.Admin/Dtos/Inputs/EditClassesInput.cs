using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Admin
{
    public class EditClassesInput
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        [Required(ErrorMessage = "班级Id不能为空")]
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        [Required(ErrorMessage = "班级名称不能为空")]
        public string ClassesName { get; set; }

        ///// <summary>
        ///// 系别Id
        ///// </summary>
        [Required(ErrorMessage = "请选择所属系别")]
        public int DeptId { get; set; }
    }
}