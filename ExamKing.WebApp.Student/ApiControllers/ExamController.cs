using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Mapster;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试接口
    /// </summary>
    public class ExamController : ApiControllerBase
    {
        private readonly IExamService _examService;
        private readonly IStudentService _studentService;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="examService"></param>
        /// <param name="studentService"></param>
        public ExamController(
            IExamService examService,
            IStudentService studentService)
        {
            _examService = examService;
            _studentService = studentService;
        }

        /// <summary>
        /// 查询正在考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamOutput>> GetExamOnlineList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamOnlineByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamOutput>>();
        }
        
        /// <summary>
        /// 查询未考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamOutput>> GetExamWaitList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamWaitByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamOutput>>();
        }
        
        /// <summary>
        /// 查询已考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamOutput>> GetExamFinshList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamFinshByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamOutput>>();
        }
        
        /// <summary>
        /// 查询已缺考列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamOutput>> GetExamNoneList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamNoneByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamOutput>>();
        }
    }
}