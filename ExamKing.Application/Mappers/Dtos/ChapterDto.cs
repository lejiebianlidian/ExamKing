namespace ExamKing.Application.Mappers
{
    public class ChapterDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }
        
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        
        /// <summary>
        /// 章节描述
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 课程
        /// </summary>
        public CourseDto Course { get; set; }
    }
}