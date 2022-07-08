using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 更新是非题输入
    /// </summary>
    public class EditJudgeInput : AddJudgeInput
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "请选择是非题")]
        public int Id { get; set; }
    }
}