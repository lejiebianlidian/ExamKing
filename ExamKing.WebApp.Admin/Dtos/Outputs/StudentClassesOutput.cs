using ExamKing.Application.Mappers;

namespace ExamKing.WebApp.Admin
{
    public class StudentClassesOutput : StudentInfoOutput
    {
        /// <summary>
        /// 班级
        /// </summary>
        public ClassesDeptOutput Classes { get; set; }
    }
}