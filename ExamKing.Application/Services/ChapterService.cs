using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.ErrorCodes;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    public class ChapterService : IChapterService, ITransient
    {
        private readonly IRepository<TbChapter> _chapterRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="chapterRepository"></param>
        public ChapterService(IRepository<TbChapter> chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        /// <summary>
        /// 创建章节
        /// </summary>
        /// <param name="chapterDto"></param>
        /// <returns></returns>
        public async Task<ChapterDto> CreateChapter(ChapterDto chapterDto)
        {
            // 判断课程是否存在
            var course = await _chapterRepository.Change<TbCourse>()
                .FirstOrDefaultAsync(u => u.Id == chapterDto.CourseId);
            if (course==null)
            {
                throw Oops.Oh(CourseErrorCodes.c1501);
            }
            var chapter = await _chapterRepository
                .InsertNowAsync(chapterDto.Adapt<TbChapter>());
            return chapter.Entity.Adapt<ChapterDto>();
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="chapterDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ChapterDto> UpdateChapter(ChapterDto chapterDto)
        {
            // 判断课程是否存在
            var course = await _chapterRepository.Change<TbCourse>()
                .FirstOrDefaultAsync(u => u.Id == chapterDto.CourseId);
            if (course==null)
            {
                throw Oops.Oh(CourseErrorCodes.c1501);
            }
            var chapter = await _chapterRepository.FirstOrDefaultAsync(u => u.Id == chapterDto
                .Id);
            if (chapter == null)
            {
                throw Oops.Oh(ChapterErrorCodes.z1601);
            }

            var changeChapter = await _chapterRepository
                .UpdateNowAsync(chapterDto.Adapt(chapter));
            return changeChapter.Entity.Adapt<ChapterDto>();
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteChapter(int chapterId)
        {
            var chapter = await _chapterRepository
                .FirstOrDefaultAsync(x => x.Id == chapterId);
            if (chapter == null)
            {
                throw Oops.Oh(ChapterErrorCodes.z1601);
            }

            await _chapterRepository.DeleteAsync(chapter);
        }

        /// <summary>
        /// 根据课程Id查询分页章节
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ChapterDto>> FindChapterAllByCourseIdPage(int courseId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _chapterRepository
                .Entities
                .AsNoTracking()
                .Where(u=>u.CourseId==courseId)
                .Select(u => new TbChapter
                {
                    Id = u.Id,
                    ChapterName = u.ChapterName,
                    CourseId = u.CourseId,
                    Desc = u.Desc ,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ChapterDto>>();
        }

        /// <summary>
        /// 根据Id查询章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ChapterDto> FindChapterById(int id)
        {
            var chapter = await _chapterRepository
                .Entities
                .Select(u => new TbChapter
                {
                    Id = u.Id,
                    ChapterName = u.ChapterName,
                    CourseId = u.CourseId,
                    Desc = u.Desc,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    }
                }).FirstOrDefaultAsync(u => u.Id == id);
            if (chapter==null)
            {
                throw Oops.Oh(ChapterErrorCodes.z1601);
            }

            return chapter.Adapt<ChapterDto>();
        }
    }
}