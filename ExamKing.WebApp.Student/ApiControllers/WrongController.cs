using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 错题接口
    /// </summary>
    public class WrongController : ApiControllerBase
    {
        private readonly IStuanswerdetailService _stuanswerdetail;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="stuanswerdetail"></param>
        public WrongController(IStuanswerdetailService stuanswerdetail)
        {
            _stuanswerdetail = stuanswerdetail;
        }

        /// <summary>
        /// 查询全部错题集合列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamWrongSubOutput>> GetWrongList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var wrongs = await _stuanswerdetail.FindWrongByStudentAndPage(
                student.Id, pageIndex, pageSize);
            
            return wrongs.Adapt<PagedList<ExamWrongSubOutput>>();
        }

        /// <summary>
        /// 获取错题本信息
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<ExamWrongSubOutput> GetWrongInfo(int examId)
        {
            var student = await GetStudent();
            var wrongInfo = await _stuanswerdetail.FindWrongInfoByExamAndStudent(
                examId, student.Id);
            return wrongInfo.Adapt<ExamWrongSubOutput>();
        }

        /// <summary>
        /// 获取错题本单选题列表
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionAnswerSubOutput>> GetWrongSingles(
            [FromQuery] int examId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var singles = await _stuanswerdetail.FindWrongSinglesByExamAndStudentAndPage(
            examId,student.Id, pageIndex,pageSize
                );

            return singles.Adapt<PagedList<ExamquestionAnswerSubOutput>>();
        }
        
        /// <summary>
        /// 获取错题本多选题列表
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionAnswerSubOutput>> GetWrongSelects(
            [FromQuery] int examId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var selects = await _stuanswerdetail.FindWrongSelectsByExamAndStudentAndPage(
                examId,student.Id, pageIndex,pageSize
            );

            return selects.Adapt<PagedList<ExamquestionAnswerSubOutput>>();
        }
        /// <summary>
        /// 获取错题本是非题列表
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionAnswerSubOutput>> GetWrongJudges(
            [FromQuery] int examId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var judges = await _stuanswerdetail.FindWrongJudgesByExamAndStudentAndPage(
                examId,student.Id, pageIndex,pageSize
            );

            return judges.Adapt<PagedList<ExamquestionAnswerSubOutput>>();
        }
    }
}