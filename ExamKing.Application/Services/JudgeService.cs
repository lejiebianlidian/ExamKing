using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 是非题 服务
    /// </summary>
    public class JudgeService : IJudgeService, ITransient
    {
        private readonly IRepository<TbJudge> _judgeRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public JudgeService(IRepository<TbJudge> judgeRepository)
        {
            _judgeRepository = judgeRepository;
        }
        
        /// <summary>
        /// 创建是非题 
        /// </summary>
        /// <param name="judgeDto"></param>
        /// <returns></returns>
        public async Task<JudgeDto> CreateJudge(JudgeDto judgeDto)
        {
            // judgeDto.CreateTime = TimeUtil.GetTimeStampNow();
            var judgeInsert = await _judgeRepository
                .InsertNowAsync(judgeDto.Adapt<TbJudge>());

            return judgeInsert.Entity.Adapt<JudgeDto>();
        }

        /// <summary>
        /// 更新是非题 
        /// </summary>
        /// <param name="judgeDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JudgeDto> UpdateJudge(JudgeDto judgeDto)
        {
            var judgeEntity = await _judgeRepository.FirstOrDefaultAsync(u => u.Id == judgeDto.Id);
            if (judgeEntity==null)
            {
                throw Oops.Oh(JudgeErrorCodes.s1801);
            }

            var judgeUpdate = judgeDto.Adapt(judgeEntity);
            await judgeUpdate
                .UpdateExcludeAsync(u=>u.CreateTime);

            return judgeUpdate.Adapt<JudgeDto>();
        }

        /// <summary>
        /// 删除是非题 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteJudge(int id)
        {
            var judgeEntity = await _judgeRepository.FirstOrDefaultAsync(u => u.Id == id);
            if (judgeEntity==null)
            {
                throw Oops.Oh(JudgeErrorCodes.s1801);
            }

            await _judgeRepository.DeleteAsync(judgeEntity);
        }
        
        /// <summary>
        /// 根据教师查询是非题分页
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<JudgeDto>> FindJudgeAllByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _judgeRepository
                .Entities
                .AsNoTracking()
                .Where(u=>u.TeacherId==teacherId)
                .Select(u => new TbJudge
                {
                    Id = u.Id,
                    Question = u.Question,
                    Answer = u.Answer,
                    CourseId = u.CourseId,
                    ChapterId = u.ChapterId,
                    TeacherId = u.TeacherId,
                    Ideas = u.Ideas,
                    CreateTime = u.CreateTime,
                    Chapter = new TbChapter
                    {
                        Id = u.Chapter.Id,
                        ChapterName = u.Chapter.ChapterName,
                    },
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName
                    },
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<JudgeDto>>();
        }

        /// <summary>
        /// 根据id查询是非题 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JudgeDto> FindJudgeById(int id)
        {
            var judgeEntity = await _judgeRepository
                .Entities
                .Select(u => new TbJudge
                {
                    Id = u.Id,
                    Question = u.Question,
                    Answer = u.Answer,
                    CourseId = u.CourseId,
                    ChapterId = u.ChapterId,
                    TeacherId = u.TeacherId,
                    Ideas = u.Ideas,
                    CreateTime = u.CreateTime,
                    Chapter = new TbChapter
                    {
                        Id = u.Chapter.Id,
                        ChapterName = u.Chapter.ChapterName,
                    },
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName
                    },
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (judgeEntity==null)
            {
                throw Oops.Oh(JudgeErrorCodes.s1801);
            }

            return judgeEntity.Adapt<JudgeDto>();
        }
        
    }
}