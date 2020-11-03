using System;
namespace ExamKing.Application.Mappers
{
    public class ClassesDto
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
        public int Deptld { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        
    }
}
