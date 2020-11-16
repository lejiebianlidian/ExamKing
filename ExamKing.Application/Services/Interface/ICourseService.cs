using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using Fur.DatabaseAccessor;
using Fur.DependencyInjection;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 课程服务
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// 分页查询课程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<CourseDto>> FindCourseAllByPage(int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 创建课程
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        public Task<CourseDto> CreateCourse(CourseDto courseDto);

        /// <summary>
        /// 更新课程
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        public Task<CourseDto> UpdateCourse(CourseDto courseDto);

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteCourse(int id);
        
        /// <summary>
        /// 查找课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<CourseDto> FindCourseById(int id);
    }
}