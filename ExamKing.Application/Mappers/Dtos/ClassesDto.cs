using System;
namespace ExamKing.Application.Mappers
{
    public class ClassesDto
    {
        public ClassesDto()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            CreateTime = Convert.ToInt64(ts.TotalSeconds).ToString();
        }

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

        /// <summary>
        /// 系别
        /// </summary>
        public DeptDto Dept { get; set; }
    }
}
