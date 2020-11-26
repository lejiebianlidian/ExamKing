using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Consts;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 考试接口
    /// </summary>
    public class ExamController : ApiControllerBase
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly IStuanswerdetailService _stuanswerdetailService;
        private readonly ISelectService _selectService;
        private readonly IJudgeService _judgeService;
        private readonly IStuscoreService _stuscoreService;
        
        /// <summary>
        /// 依赖注入 
        /// </summary>
        public ExamController(
            IExamService examService,
            IQuestionService questionService,
            IStuanswerdetailService stuanswerdetailService,
            ISelectService selectService,
            IJudgeService judgeService,
            IStuscoreService stuscoreService)
        {
            _examService = examService;
            _questionService = questionService;
            _stuanswerdetailService = stuanswerdetailService;
            _selectService = selectService;
            _judgeService = judgeService;
            _stuscoreService = stuscoreService;
        }

        /// <summary>
        /// 查询正在考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamOnlineList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamOnlineByClassesAndPage(
                student.ClassesId,
                pageIndex,
                pageSize);
            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        }
        
        /// <summary>
        /// 查询未考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamWaitList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamWaitByClassesAndPage(
                student.ClassesId,
                pageIndex,
                pageSize);
            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        }
        
        /// <summary>
        /// 查询已结束列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamFinshList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamFinshByClassesAndPage(
                student.ClassesId,
                pageIndex,
                pageSize);
            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        }

        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExamQuestionOutput> GetExamInfo(int id)
        {
            var exam = await _examService.FindExamById(id);
            return exam.Adapt<ExamQuestionOutput>();
        }

        /// <summary>
        /// 查询考试是非题
        /// </summary>
        /// <param name="examId">试卷ID</param>
        /// <param name="questionId">题目ID</param>
        /// <returns></returns>
        public async Task<ExamquestionSelectOutput> GetJudge(int examId, int questionId)
        {
            var student = await GetStudent();
            var judges = await _questionService.FindJudgeAndAnswerByExamAndStudent(examId,questionId,student
                .Id);

            return judges.Adapt<ExamquestionSelectOutput>();
        }

        /// <summary>
        /// 查询考试多选题
        /// </summary>
        /// <param name="examId">考试id</param>
        /// <param name="questionId">题目id</param>
        /// <returns></returns>
        public async Task<ExamquestionSelectOutput> GetSelect(int examId, int questionId)
        {
            var student = await GetStudent();
            var judges = await _questionService.FindSelectAndAnswerByExamAndStudent(examId,questionId,student.Id);

            return judges.Adapt<ExamquestionSelectOutput>();
        }
        
        /// <summary>
        /// 查询考试单选题
        /// </summary>
        /// <param name="examId">试卷ID</param>
        /// <param name="questionId">题目ID</param>
        /// <returns></returns>
        public async Task<ExamquestionSelectOutput> GetSingle(int examId, int questionId)
        {
            var student = await GetStudent();
            var judges = await _questionService.FindSingleAndAnswerByExamAndStudent(examId,questionId,student.Id);

            return judges.Adapt<ExamquestionSelectOutput>();
        }

        /// <summary>
        /// 提交答题结果
        /// </summary>
        /// <param name="addExamAnswerInput"></param>
        /// <returns></returns>
        public async Task<StuanswerdetailDto> SubmitExamAnswer(AddExamAnswerInput addExamAnswerInput)
        {
            var student = await GetStudent();
            var answer = await _stuanswerdetailService.AnswerQuestionByStudent(
                student.Id,
                addExamAnswerInput.questionId,
                addExamAnswerInput.answer);

            return answer;
        }
        
        /// <summary>
        /// 获取解题思路信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        public async Task<string> GetQuestionIdeas(int id, string questionType)
        {
            if (questionType==QuestionTypeConst.Judge)
            {
                var question = await _judgeService.FindJudgeById(id);
                return question.Ideas;
            }
            else
            {
                var question = await _selectService.FindSelectById(id);
                return question.Ideas;
            }
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
        /// 查询考试结果详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExamResultOutput> GetExamResult(int id)
        {
            var student = await GetStudent();
            var result = await _examService.FindExamResultByStudent(id, student.Id);
            return result.Adapt<ExamResultOutput>();
        }

        /// <summary>
        /// 交卷
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<StuscoreOutput> SubmitExamPaper(int examId)
        {
            var student = await GetStudent();
            var stuscore = await _examService.SubmitExamByStudent(examId, student.Id);
            return stuscore.Adapt<StuscoreOutput>();
        }
    }
}