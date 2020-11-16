using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.DatabaseAccessor;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 课程接口
    /// </summary>
    public class CourseController : ApiControllerBase
    {
        private readonly ICourseService _courseService;
        
        /// <summary>
        /// 依赖注入 
        /// </summary>
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// 课程列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<CourseOutput>> GetCourseList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var course = await _courseService.FindCourseAllByPage(pageIndex, pageSize);
            return course.Adapt<PagedList<CourseOutput>>();
        }
        
        /// <summary>
        /// 创建课程
        /// </summary>
        /// <param name="addCourseInput"></param>
        /// <returns></returns>
        public async Task<CourseDto> PostCreateCourse(AddCourseInput addCourseInput)
        {
            var course = await _courseService.CreateCourse(addCourseInput.Adapt<CourseDto>());
            return course;
        }

        /// <summary>
        /// 修改课程
        /// </summary>
        /// <param name="editCourseInput"></param>
        /// <returns></returns>
        public async Task<string> UpdateEditCourse(EditCourseInput editCourseInput)
        {
            await _courseService.UpdateCourse(editCourseInput.Adapt<CourseDto>());
            return "success";
        }
        
        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveCourse(int id)
        {
            await _courseService.DeleteCourse(id);
            return "success";
        }
        
        /// <summary>
        /// 查询课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CourseOutput> GetFindCourse(int id)
        {
            var course = await _courseService.FindCourseById(id);
            return course.Adapt<CourseOutput>();
        }
    }
}