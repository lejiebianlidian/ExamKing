using System;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.ErrorCodes;
using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 课程服务
    /// </summary>
    public class CourseService : ICourseService, ITransient
    {
        private readonly IRepository<TbCourse> _courseRepository;
        /// <summary>
        /// 依赖注入
        /// </summary>
        public CourseService(IRepository<TbCourse> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// 分页查询课程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<CourseDto>> FindCourseAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _courseRepository.AsQueryable(false)
                .ProjectToType<CourseDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 创建课程
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        public async Task<CourseDto> CreateCourse(CourseDto courseDto)
        {
            // 查询系别是否存在
            var dept = await _courseRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(x => x.Id == courseDto.Deptld);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }
            // 查询教师是否存在
            var teacher = await _courseRepository.Change<TbTeacher>().Entities
                .SingleOrDefaultAsync(x => x.Id == courseDto.Teacherld);
            if (teacher==null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }
            var course = await _courseRepository.InsertNowAsync(courseDto.Adapt<TbCourse
            >());
            return course.Entity.Adapt<CourseDto>();
        }

        /// <summary>
        /// 更新课程
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CourseDto> UpdateCourse(CourseDto courseDto)
        {
            var course = await _courseRepository.Entities.SingleOrDefaultAsync(x => x.Id == courseDto.Id);
            if (course==null)
            {
                throw Oops.Oh(
                    CourseErrorCodes.c1501
                );
            }
            // 查询系别是否存在
            var dept = await _courseRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(x => x.Id == course.Deptld);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }
            // 查询教师是否存在
            var teacher = await _courseRepository.Change<TbTeacher>().Entities
                .SingleOrDefaultAsync(x => x.Id == course.Teacherld);
            if (teacher==null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }
            var changeCourse = await _courseRepository.UpdateNowAsync(courseDto.Adapt(course));
            return changeCourse.Entity.Adapt<CourseDto>();
        }

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteCourse(int id)
        {
            var course = await _courseRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
            if (course==null)
            {
                throw Oops.Oh(
                    CourseErrorCodes.c1501
                );
            }

            await _courseRepository.DeleteAsync(course);
        }

        /// <summary>
        /// 查找课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CourseDto> FindCourseById(int id)
        {
            var course = await _courseRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
            if (course==null)
            {
                throw Oops.Oh(
                    CourseErrorCodes.c1501
                );
            }

            return course.Adapt<CourseDto>();
        }
    }
}