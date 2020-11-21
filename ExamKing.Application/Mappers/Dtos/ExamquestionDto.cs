using System.Text.Json.Serialization;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 试卷题目 DTO
    /// </summary>
    public class ExamquestionDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QuestionType { get; set; }
        
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int ExamId { get; set; }
        
        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionId { get; set; }
        
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
        
        /// <summary>
        /// 多选题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelectDto Select { get; set; }
        
        /// <summary>
        /// 单选题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelectDto Single { get; set; }
        
        /// <summary>
        /// 是非题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JudgeDto Judge { get; set; }
    }
}