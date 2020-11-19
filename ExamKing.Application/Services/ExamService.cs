using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Consts;
using ExamKing.Core.Entites;
using ExamKing.Core.ErrorCodes;
using ExamKing.Core.Utils;
using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 试卷服务
    /// </summary>
    public class ExamService : IExamService, ITransient
    {
        private readonly IRepository<TbExam> _examRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public ExamService(IRepository<TbExam> examRepository)
        {
            _examRepository = examRepository;
        }

        /// <summary>
        /// 手动组卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public async Task<ExamDto> CreateExam(ExamDto examDto)
        {
            var exam = await _examRepository
                .InsertNowAsync(examDto.Adapt<TbExam>());
            var examEntity = exam.Entity;
            return examEntity.Adapt<ExamDto>();
        }

        public async Task<ExamDto> AutoCreateExam(ExamDto examDto)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 更新试卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public async Task<ExamDto> UpdateExam(ExamDto examDto)
        {
            var oldExam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == examDto.Id);
            if (oldExam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            // 删除原试卷题目
            await _examRepository
                .Change<TbExamquestion>()
                .DeleteAsync(
                    _examRepository
                        .Change<TbExamquestion>()
                        .Where(u => u.ExamId == oldExam.Id, false).ToList()
                );

            // 更新试卷
            var exam = examDto.Adapt(oldExam);
            await exam
                .UpdateExcludeAsync(u=>u.CreateTime);

            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteExam(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            // 删除卷题目
            await _examRepository
                .Change<TbExamquestion>()
                .DeleteAsync(
                    _examRepository
                        .Change<TbExamquestion>()
                        .Where(u => u.ExamId == id, false).ToList()
                );
            await _examRepository.DeleteAsync(exam);
        }

        /// <summary>
        /// 根据教师查询分页试卷
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamAllByTeacherAndPage(int teacherId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _examRepository
                .Entities.AsNoTracking()
                .Where(u => u.TeacherId == teacherId)
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    CourseId = u.CourseId,
                    TeacherId = u.TeacherId,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    Duration = u.Duration,
                    IsEnable = u.IsEnable,
                    IsFinish = u.IsFinish,
                    CreateTime = u.CreateTime,
                    ExamScore = u.ExamScore,
                    JudgeScore = u.JudgeScore,
                    SingleScore = u.SingleScore,
                    SelectScore = u.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Teacher.Id,
                        TeacherName = u.Teacher.TeacherName,
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 查询试卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> FindExamById(int id)
        {
            var exam = await _examRepository
                .Entities
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    CourseId = u.CourseId,
                    TeacherId = u.TeacherId,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    Duration = u.Duration,
                    IsEnable = u.IsEnable,
                    IsFinish = u.IsFinish,
                    CreateTime = u.CreateTime,
                    ExamScore = u.ExamScore,
                    JudgeScore = u.JudgeScore,
                    SingleScore = u.SingleScore,
                    SelectScore = u.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Teacher.Id,
                        TeacherName = u.Teacher.TeacherName,
                    },
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            return exam.Adapt<ExamDto>();
        }
        
    }
}