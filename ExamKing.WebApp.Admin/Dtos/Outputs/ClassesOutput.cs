using System.Text.Json.Serialization;
using ExamKing.Application.Mappers;
using ExamKing.Core.JsonConverters;

namespace ExamKing.WebApp.Admin
{
    public class ClassesOutput
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassesName { get; set; }

        ///// <summary>
        ///// 系别Id
        ///// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DeptId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(CreateTimeConverter))]
        public string CreateTime { get; set; }
    }
}