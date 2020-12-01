using System.Text.Json.Serialization;
using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Admin
{
    
    /// <summary>
    /// 教师系别信息
    /// </summary>
    public class TeacherDeptOutput : TeacherInfoOutput
    {
        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
        
        /// <summary>
        /// 系别
        /// </summary>
        public DeptSubOutput Dept { get; set; }
    }
    
}