namespace ExamKing.WebApp.Admin
{
    public class TeacherSubOutput
    {

        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 教师工号
        /// </summary>
        public string TeacherNo { get; set; }

        
    }

    public class TeacherDeptSubOuput : TeacherSubOutput
    {
        /// <summary>
        /// 系别
        /// </summary>
        public DeptSubOutput Dept { get; set; }
    }
}