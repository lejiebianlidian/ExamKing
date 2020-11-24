using System;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Consts;
using ExamKing.Application.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 答题服务
    /// </summary>
    public class StuanswerdetailService : IStuanswerdetailService, ITransient
    {
        private readonly IRepository<TbStuanswerdetail> _answerRepository;
        private readonly IRepository<TbExamquestion> _examquestionRepository;
        private readonly ISelectService _selectService;
        private readonly IJudgeService _judgeService;
        /// <summary>
        /// 依赖注入
        /// </summary>
        public StuanswerdetailService(
            IRepository<TbStuanswerdetail> answerRepository,
            IRepository<TbExamquestion> examquestionRepository,
            ISelectService selectService,
            IJudgeService judgeService)
        {
            _answerRepository = answerRepository;
            _examquestionRepository = examquestionRepository;
            _selectService = selectService;
            _judgeService = judgeService;
        }

        /// <summary>
        /// 获取学生全部错题数量
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerByStudent(int studentId)
        {
            var count = await _answerRepository
                .Where(x => x.StuId == studentId)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// 获取学生今日错题数
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerTodayByStudent(int studentId)
        {
            var today = DateTimeOffset.UtcNow;
            var count = await _answerRepository
                .Where(x => x.StuId == studentId)
                .Where(x => x.CreateTime.Date == today.Date)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// 获取学生错题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerCountByStudent(
            int studentId,
            int examId,
            string questionType = "all")
        {
            var count =  _answerRepository
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == examId && u.StuId == studentId)
                .Where(u => u.Isright == "0");
            if (questionType!="all")
            {
                count = count.Where(u => u.QuestionType == questionType);
            }
            return await count.CountAsync();
        }

        /// <summary>
        /// 获取学生对题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <param name="questionType"></param>
        /// <returns></returns>
        public async Task<int> GetSuccessAnswerCountByStudent(
            int studentId, 
            int examId, 
            string questionType="all")
        {
            var count =  _answerRepository
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == examId && u.StuId == studentId)
                .Where(u => u.Isright == "1");
            if (questionType!="all")
            {
                count = count.Where(u => u.QuestionType == questionType);
            }
            return await count.CountAsync();
        }

        /// <summary>
        /// 学生答题
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <param name="examQuestionId">考试问题Id</param>
        /// <param name="answer">回答内容</param>
        /// <returns></returns>
        public async Task<StuanswerdetailDto> AnswerQuestionByStudent(
            int studentId, 
            int examQuestionId, 
            string[] answer)
        {
            // 判断是否已经答题过，避免重复答题
            var isAnswer = await _examquestionRepository.Change<TbStuanswerdetail>()
                .Where(u => u.StuId == studentId
                            && u.QuestionId == examQuestionId)
                .FirstOrDefaultAsync();
            if (isAnswer!=null)
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2102);
            }
            var examQuestion = await _examquestionRepository
                .Entities.AsNoTracking()
                .Where(u => u.Id == examQuestionId)
                .Select(u=> new TbExamquestion
                {
                    Id = u.Id,
                    QuestionType = u.QuestionType,
                    ExamId = u.ExamId,
                    QuestionId = u.QuestionId,
                    Score = u.Score,
                    Exam = new TbExam
                    {
                        Id = u.Exam.Id,
                        IsEnable = u.Exam.IsEnable,
                        IsFinish = u.Exam.IsFinish,
                    }
                })
                .FirstOrDefaultAsync();
            if (examQuestion==null||(examQuestion.Exam.IsEnable=="0"||examQuestion.Exam.IsFinish=="1"))
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2101);
            }
            
            string[] questionAnswer;
            // 获取题目详情
            if (examQuestion.QuestionType==QuestionTypeConst.Judge)
            {
                var question = await _judgeService.FindJudgeById(examQuestion.QuestionId);
                questionAnswer = question.Answer.Split("、");
            } else
            {
                var question = await _selectService.FindSelectById(examQuestion.QuestionId);
                questionAnswer = question.Answer.Split("、");
            }
            // 判断答案是否正确
            var q = from a in answer join b in questionAnswer on a equals b select a;
            bool isRight = answer.Length == questionAnswer.Length && q.Count() == answer.Length;
            
            var stuanswerdetailDto  = new StuanswerdetailDto
            {
                StuId = studentId,
                ExamId = examQuestion.ExamId,
                QuestionId = examQuestionId,
                QuestionType = examQuestion.QuestionType,
                Stuanswer = string.Join("、", answer),
                Answer = string.Join("、", questionAnswer),
                Isright = isRight?"1":"0",
            };
            
            // 记录答题
            var stuanswer = await _answerRepository
                .InsertNowAsync(stuanswerdetailDto.Adapt<TbStuanswerdetail>());
            
            return stuanswer.Entity.Adapt<StuanswerdetailDto>();
        }
    }
}