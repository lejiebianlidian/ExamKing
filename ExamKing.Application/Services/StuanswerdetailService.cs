using System;
using System.Collections.Generic;
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
            var count = _answerRepository
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == examId && u.StuId == studentId)
                .Where(u => u.Isright == "0");
            if (questionType != "all")
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
            string questionType = "all")
        {
            var count = _answerRepository
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == examId && u.StuId == studentId)
                .Where(u => u.Isright == "1");
            if (questionType != "all")
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
            if (isAnswer != null)
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2102);
            }

            var examQuestion = await _examquestionRepository
                .Entities.AsNoTracking()
                .Where(u => u.Id == examQuestionId)
                .Select(u => new TbExamquestion
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
            if (examQuestion == null || (examQuestion.Exam.IsEnable == "0" || examQuestion.Exam.IsFinish == "1"))
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2101);
            }

            string[] questionAnswer;
            // 获取题目详情
            if (examQuestion.QuestionType == QuestionTypeConst.Judge)
            {
                var question = await _judgeService.FindJudgeById(examQuestion.QuestionId);
                questionAnswer = question.Answer.Split("、");
            }
            else
            {
                var question = await _selectService.FindSelectById(examQuestion.QuestionId);
                questionAnswer = question.Answer.Split("、");
            }

            // 判断答案是否正确
            var q = from a in answer join b in questionAnswer on a equals b select a;
            bool isRight = answer.Length == questionAnswer.Length && q.Count() == answer.Length;

            var stuanswerdetailDto = new StuanswerdetailDto
            {
                StuId = studentId,
                ExamId = examQuestion.ExamId,
                QuestionId = examQuestionId,
                QuestionType = examQuestion.QuestionType,
                Stuanswer = string.Join("、", answer),
                Answer = string.Join("、", questionAnswer),
                Isright = isRight ? "1" : "0",
            };

            // 记录答题
            var stuanswer = await _answerRepository
                .InsertNowAsync(stuanswerdetailDto.Adapt<TbStuanswerdetail>());

            return stuanswer.Entity.Adapt<StuanswerdetailDto>();
        }

        /// <summary>
        /// 根据学生查询考试错题集列表
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindWrongByStudentAndPage(int studentId, int pageIndex = 1,
            int pageSize = 10)
        {
            var wrongs = await _answerRepository.Change<TbExam>()
                .Entities.AsNoTracking()
                .Where(u => u.IsFinish == "1")
                .Include(u => u.Stuanswerdetails.Where(x => x.StuId == studentId && x.Isright == "0"))
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    Stuanswerdetails = u.Stuanswerdetails.Select(x => new TbStuanswerdetail
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType
                    }).ToList()
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return wrongs.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据学生查询错题本详情
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<ExamDto> FindWrongInfoByExamAndStudent(int examId, int studentId)
        {
            var wrong = await _answerRepository.Change<TbExam>()
                .Entities.AsNoTracking()
                .Where(u => u.IsFinish == "1")
                .Include(u => u.Stuanswerdetails
                    .Where(x => x.StuId == studentId && x.Isright == "0" && x.ExamId == examId))
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    Stuanswerdetails = u.Stuanswerdetails.Select(s => new TbStuanswerdetail
                    {
                        Id = s.Id,
                        QuestionType = s.QuestionType,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            if (wrong == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            return wrong.Adapt<ExamDto>();
        }

        /// <summary>
        /// 学生根据考试查询单选题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionDto>> FindWrongSinglesByExamAndStudentAndPage(
            int examId, int studentId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = from q in _answerRepository.Change<TbExamquestion>().AsQueryable()
                join a in _answerRepository.Change<TbStuanswerdetail>().AsQueryable().OrderByDescending(u=>u.CreateTime) on q.Id equals a.QuestionId
                join s in _answerRepository.Change<TbSelect>().AsQueryable() on q.QuestionId equals s.Id
                where q.ExamId == examId && a.StuId == studentId && q.QuestionType == QuestionTypeConst.Single
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    Score = q.Score,
                    Stuanswerdetail = new StuanswerdetailDto
                    {
                        Id = a.Id,
                        Stuanswer = a.Stuanswer,
                        Answer = a.Answer,
                    },
                    Single = new SelectDto
                    {
                        Id = s.Id,
                        Question = s.Question,
                        Answer = s.Answer,
                        IsSingle =s.IsSingle,
                        OptionA = s.OptionA,
                        OptionB = s.OptionB,
                        OptionC = s.OptionC,
                        OptionD = s.OptionD,
                        Ideas = s.Ideas,
                        CreateTime = s.CreateTime,
                    }
                };
            var list = await pageResult.ToPagedListAsync(pageIndex, pageSize);
            return list.Adapt<PagedList<ExamquestionDto>>();
        }

        /// <summary>
        /// 学生根据考试查询多选题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionDto>> FindWrongSelectsByExamAndStudentAndPage(int examId, int studentId,
            int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = from q in _answerRepository.Change<TbExamquestion>().AsQueryable()
                join a in _answerRepository.Change<TbStuanswerdetail>().AsQueryable().OrderByDescending(u=>u.CreateTime) on q.Id equals a.QuestionId
                join s in _answerRepository.Change<TbSelect>().AsQueryable() on q.QuestionId equals s.Id
                where q.ExamId == examId && a.StuId == studentId && q.QuestionType == QuestionTypeConst.Select
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    Score = q.Score,
                    Stuanswerdetail = new StuanswerdetailDto
                    {
                        Id = a.Id,
                        Stuanswer = a.Stuanswer,
                        Answer = a.Answer,
                    },
                    Select = new SelectDto
                    {
                        Id = s.Id,
                        Question = s.Question,
                        Answer = s.Answer,
                        IsSingle =s.IsSingle,
                        OptionA = s.OptionA,
                        OptionB = s.OptionB,
                        OptionC = s.OptionC,
                        OptionD = s.OptionD,
                        Ideas = s.Ideas,
                        CreateTime = s.CreateTime,
                    }
                };
            var list = await pageResult.ToPagedListAsync(pageIndex, pageSize);
            return list.Adapt<PagedList<ExamquestionDto>>();
        }

        /// <summary>
        /// 学生根据考试查询是非题错题分页
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public async Task<PagedList<ExamquestionDto>> FindWrongJudgesByExamAndStudentAndPage(int examId, int studentId,
            int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = from q in _answerRepository.Change<TbExamquestion>().AsQueryable()
                join a in _answerRepository.Change<TbStuanswerdetail>().AsQueryable().OrderByDescending(u=>u.CreateTime) on q.Id equals a.QuestionId
                join j in _answerRepository.Change<TbJudge>().AsQueryable() on q.QuestionId equals j.Id
                where q.ExamId == examId && a.StuId == studentId && q.QuestionType == QuestionTypeConst.Judge
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    Score = q.Score,
                    Stuanswerdetail = new StuanswerdetailDto
                    {
                        Id = a.Id,
                        Stuanswer = a.Stuanswer,
                        Answer = a.Answer,
                    },
                    Judge = new JudgeDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime
                    }
                };
            var list = await pageResult.ToPagedListAsync(pageIndex, pageSize);
            return list.Adapt<PagedList<ExamquestionDto>>();
        }
    }
}