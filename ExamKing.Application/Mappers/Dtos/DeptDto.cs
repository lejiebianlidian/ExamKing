using System;
using System.Collections.Generic;

namespace ExamKing.Application.Mappers
{
    public class DeptDto
    {

        /// <summary>
        /// 系别Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系别名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        
        /// <summary>
        /// 关联班级
        /// </summary>
        public ICollection<ClassesDto> Classes { get; set; }
        
    }
}
