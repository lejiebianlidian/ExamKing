using System.Collections.Generic;
using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Admin
{
    public class DeptOutput : DeptDto
    {
        /// <summary>
        /// 关联班级
        /// </summary>
        public List<ClassesOutput> Classes { get; set; }
    }
    
}