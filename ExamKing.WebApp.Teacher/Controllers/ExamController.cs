using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using ExamKing.Core.Consts;
using Fur.DatabaseAccessor;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 试卷接口
    /// </summary>
    public class ExamController : ApiControllerBase
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;

        /// <summary>
        /// 依赖注入 
        /// </summary>
        public ExamController(
            IExamService examService,
            IQuestionService questionService)
        {
            _examService = examService;
            _questionService = questionService;
        }

        /// <summary>
        /// 手动组卷
        /// </summary>
        /// <param name="addExamInput"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ExamOutput> InsertAddExam(AddExamInput addExamInput)
        {
            var addExamDto = addExamInput.Adapt<ExamDto>();
            addExamDto.TeacherId = GetUserId();
            var questions = new List<ExamquestionDto>();
            foreach (var item in addExamInput.Selects)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Select;
                questions.Add(q);
            }

            foreach (var item in addExamInput.Singles)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Single;
                questions.Add(q);
            }

            foreach (var item in addExamInput.Judges)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Judge;
                questions.Add(q);
            }

            addExamDto.Examquestions = questions;
            var exam = await _examService.CreateExam(addExamDto);
            return exam.Adapt<ExamOutput>();
        }


        /// <summary>
        /// 更新试卷
        /// </summary>
        /// <param name="addExamInput"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ExamOutput> UpdateEditExam(EditExamInput editExamInput)
        {
            var addExamDto = editExamInput.Adapt<ExamDto>();
            addExamDto.TeacherId = GetUserId();
            var questions = new List<ExamquestionDto>();
            foreach (var item in editExamInput.Selects)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Select;
                questions.Add(q);
            }

            foreach (var item in editExamInput.Singles)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Single;
                questions.Add(q);
            }

            foreach (var item in editExamInput.Judges)
            {
                var q = item.Adapt<ExamquestionDto>();
                q.QuestionType = QuestionTypeConst.Judge;
                questions.Add(q);
            }

            addExamDto.Examquestions = questions;
            var exam = await _examService.UpdateExam(addExamDto);
            return exam.Adapt<ExamOutput>();
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<string> DeleteRemoveExam(int id)
        {
            await _examService.DeleteExam(id);
            return "success";
        }

        /// <summary>
        /// 查询考试列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamCourseOutput>> GetExamList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var teacherId = GetUserId();
            var exams = await _examService.FindExamAllByTeacherAndPage(teacherId, pageIndex, pageSize);
            return exams.Adapt<PagedList<ExamCourseOutput>>();
        }

        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExamCourseOutput> GetExamInfo(int id)
        {
            var exams = await _examService.FindExamById(id);
            return exams.Adapt<ExamCourseOutput>();
        }

        /// <summary>
        /// 查询考试是非题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionOutput>> GetJudes(
            [FromQuery] int id,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var judges = await _questionService.FindJudgeByExamAndPage(id, pageIndex, pageSize);

            return judges.Adapt<PagedList<ExamquestionOutput>>();
        }
        
        /// <summary>
        /// 查询考试多选题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionOutput>> GetSelects(
            [FromQuery] int id,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var judges = await _questionService.FindSelectByExamAndPage(id, pageIndex, pageSize);

            return judges.Adapt<PagedList<ExamquestionOutput>>();
        }
        
        /// <summary>
        /// 查询考试单选题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionOutput>> GetSingles(
            [FromQuery] int id,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var judges = await _questionService.FindSingleByExamAndPage(id, pageIndex, pageSize);

            return judges.Adapt<PagedList<ExamquestionOutput>>();
        }
    }
}