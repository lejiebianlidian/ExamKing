using System;
namespace ExamKing.Application.Mappers
{
    public class DeptDto
    {
        public DeptDto()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            CreateTime = Convert.ToInt64(ts.TotalSeconds).ToString();
        }

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
    }
}
