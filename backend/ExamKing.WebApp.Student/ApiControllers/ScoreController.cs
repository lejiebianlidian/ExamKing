using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 成绩接口
    /// </summary>
    public class ScoreController : ApiControllerBase
    {
        private readonly IStuscoreService _stuscoreService;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="stuscoreService"></param>
        public ScoreController(IStuscoreService stuscoreService)
        {
            _stuscoreService = stuscoreService;
        }

        
        /// <summary>
        /// 查询学生成绩列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<StuscoreExamOutput>> GetExamScoreList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var scores = await _stuscoreService.FindScoreAllByStudentAndPage(
                student.Id,
                pageIndex,
                pageSize);
            return scores.Adapt<PagedList<StuscoreExamOutput>>();
        }
        
        /// <summary>
        /// 查询考试成绩信息
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<StuscoreOutput> FindExamScore(int examId)
        {
            var student =await GetStudent();
            var score = await _stuscoreService.FindExamScoreByStudent(examId, student.Id);
            return score.Adapt<StuscoreOutput>();
        }
        
    }
}