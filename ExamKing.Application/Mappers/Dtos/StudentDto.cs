using System;
namespace ExamKing.Application.Mappers
{
    public class StudentDto
    {

        /// <summary>
        /// 学生ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StuName { get; set; }

        /// <summary>
        /// 系别Id
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public int ClassesId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StuNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        public ClassesDto Classes { get; set; }

        /// <summary>
        /// 系别
        /// </summary>
        public DeptDto Dept { get; set; }

    }
}
