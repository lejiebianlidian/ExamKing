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
            var pageResult = await _courseRepository
                .Entities.AsNoTracking()
                .Select(u => new TbCourse
                {
                    Id = u.Id,
                    CourseName = u.CourseName,
                    DeptId = u.DeptId,
                    TeacherId = u.TeacherId,
                    CreateTime = u.CreateTime,
                    Teacher = new TbTeacher
                    {
                        TeacherName = u.Teacher.TeacherName,
                        TeacherNo = u.Teacher.TeacherNo,
                    },
                    Classes = u.Classes.Select(u=>new TbClass
                    {
                        Id = u.Id,
                        ClassesName = u.ClassesName,
                    }).ToList()
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<CourseDto>>();
        }

        /// <summary>
        /// 创建课程
        /// </summary>
        /// <param name="classesIds"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        public async Task<CourseDto> CreateCourse(int[] classesIds, CourseDto courseDto)
        {
            // 查询系别是否存在
            var dept = await _courseRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(x => x.Id == courseDto.DeptId);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }
            
            // 查询教师是否存在
            var teacher = await _courseRepository.Change<TbTeacher>().Entities
                .SingleOrDefaultAsync(x => x.Id == courseDto.TeacherId);
            if (teacher == null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }
            
            courseDto.CreateTime = TimeUtil.GetTimeStampNow();
            var course = await _courseRepository
                .InsertNowAsync(courseDto.Adapt<TbCourse
            >());
            // 为课程分配班级
            var courseClasses = new List<TbCourseclass>();
            foreach (var item in classesIds)
            {
                courseClasses.Add(new TbCourseclass
                {
                    CourseId = course.Entity.Id,
                    ClassesId = item
                });
            }
            await _courseRepository.Change<TbCourseclass>().InsertAsync(courseClasses);
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
            if (course == null)
            {
                throw Oops.Oh(
                    CourseErrorCodes.c1501
                );
            }

            // 查询系别是否存在
            var dept = await _courseRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(x => x.Id == course.DeptId);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }

            // 查询教师是否存在
            var teacher = await _courseRepository.Change<TbTeacher>().Entities
                .SingleOrDefaultAsync(x => x.Id == course.TeacherId);
            if (teacher == null)
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
            if (course == null)
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
            var course = await _courseRepository
                .Entities
                .Select(u => new TbCourse
                {
                    Id = u.Id,
                    CourseName = u.CourseName,
                    DeptId = u.DeptId,
                    TeacherId = u.TeacherId,
                    CreateTime = u.CreateTime,
                    Teacher = new TbTeacher
                    {
                        TeacherName = u.Teacher.TeacherName,
                        TeacherNo = u.Teacher.TeacherNo,
                    },
                    Classes = u.Classes.Select(u=>new TbClass
                    {
                        Id = u.Id,
                        ClassesName = u.ClassesName,
                    }).ToList()
                })
                .SingleOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw Oops.Oh(
                    CourseErrorCodes.c1501
                );
            }

            return course.Adapt<CourseDto>();
        }

        /// <summary>
        /// 根据教师查询分页课程
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<CourseDto>> FindCourseAllByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _courseRepository
                .Entities.AsNoTracking()
                .Where(u=>u.TeacherId == teacherId)
                .Select(u => new TbCourse
                {
                    Id = u.Id,
                    CourseName = u.CourseName,
                    DeptId = u.DeptId,
                    TeacherId = u.TeacherId,
                    CreateTime = u.CreateTime
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<CourseDto>>();
        }

        public async Task<bool> HasTeacher(int teacherId, int courseId)
        {
            var teacher = await _courseRepository
                .FirstOrDefaultAsync(u => u.TeacherId == teacherId && u.Id == courseId);
            if (teacher==null)
            {
                throw Oops.Oh(CourseErrorCodes.c1501);
            }

            return true;
        }
    }
}