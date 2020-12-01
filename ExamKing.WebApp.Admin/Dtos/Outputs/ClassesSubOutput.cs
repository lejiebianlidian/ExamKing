namespace ExamKing.WebApp.Admin
{
    public class ClassesSubOutput
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string classesName { get; set; }

    }
    
    public class ClassesDeptSubOutput : ClassesSubOutput
    {
        /// <summary>
        /// 系别
        /// </summary>
        public DeptSubOutput Dept { get; set; }
    }
}