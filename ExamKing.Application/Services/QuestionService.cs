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
    /// 题库服务
    /// </summary>
    public class QuestionService : IQuestionService, ITransient
    {
        private readonly IRepository<TbExamquestion> _repository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public QuestionService(IRepository<TbExamquestion> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 根据考试查询是非题分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionDto>> FindJudgeByExamAndPage(int id, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = from j in _repository.Change<TbJudge>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Judge) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
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

        /// <summary>
        /// 根据考试查询多选题分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamquestionDto>> FindSelectByExamAndPage(int id, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Select) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Select = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var list = await pageResult.ToPagedListAsync(pageIndex, pageSize);
            return list.Adapt<PagedList<ExamquestionDto>>();
        }

        /// <summary>
        /// 根据考试查询单选题分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagedList<ExamquestionDto>> FindSingleByExamAndPage(int id, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Single) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Single = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var list = await pageResult.ToPagedListAsync(pageIndex, pageSize);
            return list.Adapt<PagedList<ExamquestionDto>>();
        }

        /// <summary>
        /// 根据考试查询全部是非题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ExamquestionDto>> FindJudgeByExam(int id)
        {
            var pageResult = from j in _repository.Change<TbJudge>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Judge) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Judge = new JudgeDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime
                    }
                };
            var list = await pageResult.ToListAsync();
            return list.Adapt<List<ExamquestionDto>>();
        }

        /// <summary>
        /// 根据考试查询全部选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ExamquestionDto>> FindSelectByExam(int id)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Select) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Select = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var list = await pageResult.ToListAsync();
            return list.Adapt<List<ExamquestionDto>>();
        }

        /// <summary>
        /// 根据考试查询全部单选题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ExamquestionDto>> FindSingleByExam(int id)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Single) && q.ExamId == id
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Single = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var list = await pageResult.ToListAsync();
            return list.Adapt<List<ExamquestionDto>>();
        }

        /// <summary>
        /// 根据考试查询学生是非题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public async Task<ExamquestionDto> FindJudgeAndAnswerByExamAndStudent(int id, int questionId, int studentId)
        {
            var pageResult = from j in _repository.Change<TbJudge>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Judge) && q.ExamId == id && q.Id == questionId
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Judge = new JudgeDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime
                    }
                };
            var question = await pageResult.FirstOrDefaultAsync();
            if (question == null)
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2101);
            }
            // 查询答题详情
            var answer = await _repository.Change<TbStuanswerdetail>()
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == id && u.StuId == studentId && u.QuestionId == questionId)
                .Select(s=>new TbStuanswerdetail
                {
                    Id = s.Id,
                    StuId = s.StuId,
                    ExamId = s.ExamId,
                    QuestionId = s.QuestionId,
                    QuestionType = s.QuestionType,
                    Stuanswer = s.Stuanswer,
                    Answer = s.Answer,
                    Isright = s.Isright,
                    CreateTime = s.CreateTime
                })
                .FirstOrDefaultAsync();
            question.Stuanswerdetail = answer.Adapt<StuanswerdetailDto>();
            return question.Adapt<ExamquestionDto>();
        }

        /// <summary>
        /// 根据考试查询学生多选题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public async Task<ExamquestionDto> FindSelectAndAnswerByExamAndStudent(int id, int questionId, int studentId)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Select) && q.ExamId == id && q.Id == questionId
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Select = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var question = await pageResult.FirstOrDefaultAsync();
            if (question == null)
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2101);
            }
            // 查询答题详情
            var answer = await _repository.Change<TbStuanswerdetail>()
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == id && u.StuId == studentId && u.QuestionId == questionId)
                .Select(s=>new TbStuanswerdetail
                {
                    Id = s.Id,
                    StuId = s.StuId,
                    ExamId = s.ExamId,
                    QuestionId = s.QuestionId,
                    QuestionType = s.QuestionType,
                    Stuanswer = s.Stuanswer,
                    Answer = s.Answer,
                    Isright = s.Isright,
                    CreateTime = s.CreateTime
                })
                .FirstOrDefaultAsync();
            question.Stuanswerdetail = answer.Adapt<StuanswerdetailDto>();
            return question.Adapt<ExamquestionDto>();
        }

        /// <summary>
        /// 根据考试查询学生单选题和答题信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="questionId">题目Id</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public async Task<ExamquestionDto> FindSingleAndAnswerByExamAndStudent(int id, int questionId, int studentId)
        {
            var pageResult = from j in _repository.Change<TbSelect>().AsQueryable()
                join q in _repository.AsQueryable() on j.Id equals q.QuestionId
                where q.QuestionType.Equals(QuestionTypeConst.Single) && q.ExamId == id && q.Id == questionId
                select new ExamquestionDto
                {
                    Id = q.Id,
                    QuestionType = q.QuestionType,
                    ExamId = q.ExamId,
                    QuestionId = q.QuestionId,
                    Score = q.Score,
                    Single = new SelectDto
                    {
                        Id = j.Id,
                        Question = j.Question,
                        Answer = j.Answer,
                        IsSingle = j.IsSingle,
                        OptionA = j.OptionA,
                        OptionB = j.OptionB,
                        OptionC = j.OptionC,
                        OptionD = j.OptionD,
                        Ideas = j.Ideas,
                        CreateTime = j.CreateTime,
                    }
                };
            var question = await pageResult.FirstOrDefaultAsync();
            if (question == null)
            {
                throw Oops.Oh(ExamAnswerScoreErrorCodes.d2101);
            }
            // 查询答题详情
            var answer = await _repository.Change<TbStuanswerdetail>()
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == id && u.StuId == studentId && u.QuestionId == questionId)
                .Select(s=>new TbStuanswerdetail
                {
                    Id = s.Id,
                    StuId = s.StuId,
                    ExamId = s.ExamId,
                    QuestionId = s.QuestionId,
                    QuestionType = s.QuestionType,
                    Stuanswer = s.Stuanswer,
                    Answer = s.Answer,
                    Isright = s.Isright,
                    CreateTime = s.CreateTime
                })
                .FirstOrDefaultAsync();
            question.Stuanswerdetail = answer.Adapt<StuanswerdetailDto>();
            return question.Adapt<ExamquestionDto>();
        }
    }
}