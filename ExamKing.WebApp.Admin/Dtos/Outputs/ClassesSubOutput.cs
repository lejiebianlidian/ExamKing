namespace ExamKing.WebApp.Admin
{
    public class ClassesSubOutput
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        public string classesName { get; set; }

        /// <summary>
        /// 系别
        /// </summary>
        public DeptSubOutput Dept { get; set; }
    }
}