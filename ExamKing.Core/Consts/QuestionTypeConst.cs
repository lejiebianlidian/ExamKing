using Furion.DependencyInjection;

namespace ExamKing.Core.Consts
{
    /// <summary>
    /// 题型常量
    /// </summary>
    [SkipScan]
    public static class QuestionTypeConst
    {
        /// <summary>
        /// 多选题
        /// </summary>
        public const string Select = "select";
        
        /// <summary>
        /// 单选题
        /// </summary>
        public const string Single = "single";
        
        /// <summary>
        /// 是非题
        /// </summary>
        public const string Judge = "judge";
    }
}