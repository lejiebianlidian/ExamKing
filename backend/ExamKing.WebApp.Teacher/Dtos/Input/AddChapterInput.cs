using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    public class AddChapterInput
    {

        /// <summary>
        /// 章节名称
        /// </summary>
        [Required(ErrorMessage = "请输入章节名称")]
        public string ChapterName { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        [Required(ErrorMessage = "请选择所属课程")]
        public int CourseId { get; set; }

        /// <summary>
        /// 章节描述
        /// </summary>
        [Required(ErrorMessage = "请输入章节描述")]
        public string Desc { get; set; }

    }
}