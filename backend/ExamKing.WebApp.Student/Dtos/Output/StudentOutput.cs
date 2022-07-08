using System;
using System.Text.Json.Serialization;
using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentOutput : StudentDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public override string Password { get; set; }
        
        /// <summary>
        /// 班级
        /// </summary>
        public ClassesSubOutput Classes { get; set; }
        
    }
}