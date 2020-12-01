using System.Text.Json.Serialization;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 错题信息输出
    /// </summary>
    public class ExamquestionAnswerSubOutput
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
        /// 分数
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 题目信息
        /// </summary>
        public StuanswerdetailSubOutput Stuanswerdetail { get; set; }
        
        /// <summary>
        /// 多选题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelectOutput Select { get; set; }

        /// <summary>
        /// 单选题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelectOutput Single { get; set; }

        /// <summary>
        /// 是非题
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JudgeOutput Judge { get; set; }
    }
    
}