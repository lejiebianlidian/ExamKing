using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 课程接口
    /// </summary>
    public class CourseController : ApiControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IChapterService _chapterService;
        /// <summary>
        /// 依赖注入 
        /// </summary>
        public CourseController(
            ICourseService courseService,
            IChapterService chapterService)
        {
            _courseService = courseService;
            _chapterService = chapterService;
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<CourseOutput>> GetCourseList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var teacher = await GetTeacher();
            var course = await _courseService.FindCourseAllByTeacherAndPage(teacher.Id, pageIndex, pageSize);
            return course.Adapt<PagedList<CourseOutput>>();
        }

        /// <summary>
        /// 创建章节
        /// </summary>
        /// <param name="addChapterInput"></param>
        /// <returns></returns>
        public async Task<ChapterOutput> InsertAddChapter(AddChapterInput addChapterInput)
        {
            var chapter = await _chapterService.CreateChapter(addChapterInput.Adapt<ChapterDto
            >());
            return chapter.Adapt<ChapterOutput>();
        }
        
        /// <summary>
        /// 获取课程章节列表
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ChapterOutput>> GetChapterList(
            [FromQuery] int courseId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var teacher = await GetTeacher();
            // 判断该课程是否属于此教师
            await _courseService.HasTeacher(teacher.Id, courseId);
            
            var chapters = await _chapterService.FindChapterAllByCourseIdPage(courseId, pageIndex, pageSize);
            return chapters.Adapt<PagedList<ChapterOutput>>();
        }
        
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveChapter(int id)
        {
            await _chapterService.DeleteChapter(id);
            return "success";
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="editChapterInput"></param>
        /// <returns></returns>
        public async Task<ChapterOutput> UpdateEditChapter(EditChapterInput editChapterInput)
        {
            var editChapter = await _chapterService.UpdateChapter(editChapterInput.Adapt<ChapterDto>());
            return editChapter.Adapt<ChapterOutput>();
        }

        /// <summary>
        /// 查询章节信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChapterOutput> GetInfo(int id)
        {
            var chapter = await _chapterService.FindChapterById(id);
            return chapter.Adapt<ChapterOutput>();
        }
    }
}