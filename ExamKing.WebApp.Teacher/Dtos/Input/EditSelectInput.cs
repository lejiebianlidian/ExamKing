using System.ComponentModel.DataAnnotations;

namespace ExamKing.WebApp.Teacher
{
    public class EditSelectInput : AddSelectInput
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
    }
}