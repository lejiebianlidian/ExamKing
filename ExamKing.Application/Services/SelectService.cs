using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
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
    /// 选择题服务
    /// </summary>
    public class SelectService : ISelectService, ITransient
    {
        private readonly IRepository<TbSelect> _selectRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public SelectService(IRepository<TbSelect> selectRepository)
        {
            _selectRepository = selectRepository;
        }
        
        /// <summary>
        /// 创建选择题
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public async Task<SelectDto> CreateSelect(SelectDto selectDto)
        {
            // selectDto.CreateTime = TimeUtil.GetTimeStampNow();
            var selectInsert = await _selectRepository
                .InsertNowAsync(selectDto.Adapt<TbSelect>());

            return selectInsert.Entity.Adapt<SelectDto>();
        }

        /// <summary>
        /// 更新选择题
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SelectDto> UpdateSelect(SelectDto selectDto)
        {
            var selectEntity = await _selectRepository.FirstOrDefaultAsync(u => u.Id == selectDto.Id);
            if (selectEntity==null)
            {
                throw Oops.Oh(SelectErrorCodes.x1701);
            }

            var selectUpdate = selectDto.Adapt(selectEntity);
            await selectUpdate
                .UpdateExcludeAsync(u=>u.CreateTime);

            return selectUpdate.Adapt<SelectDto>();
        }

        /// <summary>
        /// 删除选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteSelect(int id)
        {
            var selectEntity = await _selectRepository.FirstOrDefaultAsync(u => u.Id == id);
            if (selectEntity==null)
            {
                throw Oops.Oh(SelectErrorCodes.x1701);
            }

            await _selectRepository.DeleteAsync(selectEntity);
        }
        
        /// <summary>
        /// 根据教师查询选择题分页
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<SelectDto>> FindSelectAllByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _selectRepository
                .Entities
                .AsNoTracking()
                .Where(u=>u.TeacherId==teacherId)
                .Select(u => new TbSelect
                {
                    Id = u.Id,
                    Question = u.Question,
                    Answer = u.Answer,
                    IsSingle = u.IsSingle,
                    CourseId = u.CourseId,
                    ChapterId = u.ChapterId,
                    TeacherId = u.TeacherId,
                    OptionA = u.OptionA,
                    OptionB = u.OptionB,
                    OptionC = u.OptionC,
                    OptionD = u.OptionD,
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

            return pageResult.Adapt<PagedList<SelectDto>>();
        }

        /// <summary>
        /// 根据id查询选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SelectDto> FindSelectById(int id)
        {
            var selectEntity = await _selectRepository
                .Entities
                .Select(u => new TbSelect
                {
                    Id = u.Id,
                    Question = u.Question,
                    Answer = u.Answer,
                    IsSingle = u.IsSingle,
                    CourseId = u.CourseId,
                    ChapterId = u.ChapterId,
                    TeacherId = u.TeacherId,
                    OptionA = u.OptionA,
                    OptionB = u.OptionB,
                    OptionC = u.OptionC,
                    OptionD = u.OptionD,
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
            if (selectEntity==null)
            {
                throw Oops.Oh(SelectErrorCodes.x1701);
            }

            return selectEntity.Adapt<SelectDto>();
        }
    }
}