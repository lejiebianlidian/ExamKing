using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 章节服务
    /// </summary>
    public interface IChapterService
    {
        /// <summary>
        /// 创建章节
        /// </summary>
        /// <param name="chapterDto"></param>
        /// <returns></returns>
        public Task<ChapterDto> CreateChapter(ChapterDto chapterDto);

        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="chapterDto"></param>
        /// <returns></returns>
        public Task<ChapterDto> UpdateChapter(ChapterDto chapterDto);

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public Task DeleteChapter(int chapterId);

        /// <summary>
        /// 根据课程Id查询分页章节
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ChapterDto>> FindChapterAllByCourseIdPage(int courseId, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据Id查询章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ChapterDto> FindChapterById(int id);
    }
}