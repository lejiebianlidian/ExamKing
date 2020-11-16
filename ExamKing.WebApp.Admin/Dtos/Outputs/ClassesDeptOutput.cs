using System.Text.Json.Serialization;
using ExamKing.Application.Mappers;
using ExamKing.Core.JsonConverters;

namespace ExamKing.WebApp.Admin
{
    public class ClassesDeptOutput : ClassesDto
    {
        /// <summary>
        /// 系别
        /// </summary>
        public DeptSubOutput Dept { get; set; }
    }

    
}