using System;
using System.Text.Json.Serialization;
using ExamKing.Core.JsonConverters;

namespace ExamKing.Application.Mappers
{
    /// <summary>
    /// 管理员DTO
    /// </summary>
    public class AdminDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTimeOffset CreateTime { get; set; }
    }
}