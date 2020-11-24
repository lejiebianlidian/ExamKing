using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Consts;
using ExamKing.Application.Mappers;
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
        private readonly IQuestionService _questionService;
        private readonly IStuanswerdetailService _stuanswerdetailService;
        private readonly ISelectService _selectService;
        private readonly IJudgeService _judgeService;
        
        /// <summary>
        /// 依赖注入 
        /// </summary>
        public ExamController(
            IExamService examService,
            IQuestionService questionService,
            IStuanswerdetailService stuanswerdetailService,
            ISelectService selectService,
            IJudgeService judgeService)
        {
            _examService = examService;
            _questionService = questionService;
            _stuanswerdetailService = stuanswerdetailService;
            _selectService = selectService;
            _judgeService = judgeService;
        }

        /// <summary>
        /// 查询正在考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamOnlineList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamOnlineByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        }
        
        /// <summary>
        /// 查询未考试列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamWaitList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamWaitByClassesAndPage(student.ClassesId);
            return exams.Adapt<PagedList<ExamQuestionOutput>>();
        }
        
        /// <summary>
        /// 查询已结束列表
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<ExamQuestionOutput>> GetExamFinshList()
        {
            var student = await GetStudent();
            var exams = await _examService.FindExamFinshByClassesAndPage(student.ClassesId);
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
        /// 查询考试是非题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <returns></returns>
        public async Task<List<ExamquestionSelectOutput>> GetJudges(int id)
        {
            var judges = await _questionService.FindJudgeByExam(id);

            return judges.Adapt<List<ExamquestionSelectOutput>>();
        }
        
        /// <summary>
        /// 查询考试多选题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <returns></returns>
        public async Task<List<ExamquestionSelectOutput>> GetSelects(int id)
        {
            var judges = await _questionService.FindSelectByExam(id);

            return judges.Adapt<List<ExamquestionSelectOutput>>();
        }
        
        /// <summary>
        /// 查询考试单选题列表
        /// </summary>
        /// <param name="id">考试id</param>
        /// <returns></returns>
        public async Task<List<ExamquestionSelectOutput>> GetSingles(int id)
        {
            var judges = await _questionService.FindSingleByExam(id);

            return judges.Adapt<List<ExamquestionSelectOutput>>();
        }

        /// <summary>
        /// 查询考试是非及答题信息列表
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <returns></returns>
        public async Task<List<ExamQuestionAnswerOutput>> GetJudgesAnswer(int id)
        {
            var student = await GetStudent();
            var judges = await _questionService.FindJudgeAndAnswerByExamAndStudent(
                id,
                student.Id);
            return judges.Adapt<List<ExamQuestionAnswerOutput>>();
        }
        
        /// <summary>
        /// 查询考试单选题及答题信息列表
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <returns></returns>
        public async Task<List<ExamQuestionAnswerOutput>> GetSinglesAnswer(int id)
        {
            var student = await GetStudent();
            var singles = await _questionService.FindSingleAndAnswerByExamAndStudent(
                id,
                student.Id);
            return singles.Adapt<List<ExamQuestionAnswerOutput>>();
        }
        
        /// <summary>
        /// 查询考试多选题及答题信息列表
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <returns></returns>
        public async Task<List<ExamQuestionAnswerOutput>> GetSelectsAnswer(int id)
        {
            var student = await GetStudent();
            var selects = await _questionService.FindSelectAndAnswerByExamAndStudent(
                id,
                student.Id);
            return selects.Adapt<List<ExamQuestionAnswerOutput>>();
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
        /// 获取考试错题数量
        /// </summary>
        /// <param name="id">考试id</param>
        /// <param name="questionType">题目类型</param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerCount(int id, string questionType)
        {
            var student = await GetStudent();
            return await _stuanswerdetailService.GetWrongAnswerCountByStudent(student.Id, id, questionType);
        }

        /// <summary>
        /// 获取考试对题数量
        /// </summary>
        /// <param name="id">考试id</param>
        /// <param name="questionType">题目类型</param>
        /// <returns></returns>
        public async Task<int> GetSuccessAnswerCount(int id, string questionType)
        {
            var student = await GetStudent();
            return await _stuanswerdetailService.GetSuccessAnswerCountByStudent(student.Id, id, questionType);
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
        
    }
}