using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 创建是非题输入
    /// </summary>
    public class AddJudgeInput
    {
        /// <summary>
        /// 问题
        /// </summary>
        [Required(ErrorMessage = "请输入问题")]
        public string Question { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        [Required(ErrorMessage = "请选择答案")]
        public string Answer { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属课程")]
        public int CourseId { get; set; }
        /// <summary>
        /// 课程章节ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属课程章节")]
        public int ChapterId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属教师")]
        public int TeacherId { get; set; }
        /// <summary>
        /// 解题思路
        /// </summary>
        public string Ideas { get; set; }
    }
}