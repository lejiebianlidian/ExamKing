#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 首页接口
    /// </summary>
    public class IndexController : ApiControllerBase
    {
        private readonly IStuanswerdetailService _stuanswerdetailService;
        private readonly IStuscoreService _stuscoreService;
        private readonly IExamService _examService;
        
        /// <inheritdoc />
        public IndexController(
            IStuanswerdetailService stuanswerdetailService,
            IStuscoreService stuscoreService,
            IExamService examService)
        {
            _stuanswerdetailService = stuanswerdetailService;
            _stuscoreService = stuscoreService;
            _examService = examService;
        }
        
        /// <summary>
        /// 获取全部错题数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerCount()
        {
            var student = await GetStudent();
            return await _stuanswerdetailService.GetWrongAnswerByStudent(student.Id);
        }

        /// <summary>
        /// 获取今日错题数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerTodayCount()
        {
            var student = await GetStudent();
            return await _stuanswerdetailService.GetWrongAnswerTodayByStudent(student.Id);
        }

        /// <summary>
        /// 获取最新一条成绩
        /// </summary>
        /// <returns></returns>
        public async Task<StuscoreOutput?> GetExamScore()
        {
            var stu = await GetStudent();
            var examScore = await _stuscoreService.FindScoreTodayByStudent(stu
                .Id);
            
            return examScore?.Adapt<StuscoreOutput>();
        }

        /// <summary>
        /// 搜索考试列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetSearchExam(
            [FromQuery] string keyword
            )
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamByKeywordAndStudentAndPage(student.ClassesId, keyword);

            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        } 
    }
}