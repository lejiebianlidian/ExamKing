namespace ExamKing.WebApp.Admin
{
    
    /// <summary>
    /// 教师信息
    /// </summary>
    public class TeacherInfoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }
        
        /// <summary>
        /// 教师工号
        /// </summary>
        public string TeacherNo { get; set; }
        
        /// <summary>
        /// 系别Id
        /// </summary>
        public int DeptId { get; set; }
        
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}