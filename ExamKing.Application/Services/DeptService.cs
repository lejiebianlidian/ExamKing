using System;
using System.Collections.Generic;
using System.Linq;
using Fur.DatabaseAccessor;
using System.Threading.Tasks;
using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using ExamKing.Core.ErrorCodes;
using ExamKing.Core.Utils;
using Mapster;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 系别服务
    /// </summary>
    public class DeptService : IDeptService, ITransient
    {

        private readonly IRepository<TbDept> _deptRepository;
        private readonly IRepository<TbTeacher> _teacherRepository;

        ///  <summary>
        /// 构造函数
        ///  </summary>
        ///  <param name="deptRepository"></param>
        ///  <param name="teacherRepository"></param>
        public DeptService(
            IRepository<TbDept> deptRepository,
            IRepository<TbTeacher> teacherRepository)
        {
            _deptRepository = deptRepository;
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// 查询全部系别和班级
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptDto>> FindDeptAll()
        {
            var depts = await _deptRepository
                .Entities.AsNoTracking()
                .Select(u => new TbDept
                {
                    Id = u.Id,
                    DeptName = u.DeptName,
                    CreateTime = u.CreateTime,
                    Classes = u.Classes.Select(c=>new TbClass
                    {
                        Id=c.Id,
                        ClassesName=c.ClassesName,
                        CreateTime=c.CreateTime,
                    }).ToList()
                })
                .ToListAsync();
            
            return depts.Adapt<List<DeptDto>>();
        }

        /// <summary>
        /// 分页查询系别
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<DeptDto>> FindDeptAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _deptRepository
                .Entities.AsNoTracking()
                .Select(u => new TbDept
                {
                    Id = u.Id,
                    DeptName = u.DeptName,
                    CreateTime = u.CreateTime,
                    Classes = u.Classes.Select(c => new TbClass
                    {
                        Id = c.Id,
                        ClassesName = c.ClassesName,
                        CreateTime = c.CreateTime,
                    }).ToList()
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<DeptDto>>();
        }

        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public async Task<DeptDto> InsertDept(DeptDto deptDto)
        {
            // deptDto.CreateTime = TimeUtil.GetTimeStampNow();
            var dept = await _deptRepository.InsertNowAsync(deptDto.Adapt<TbDept>());
            return dept.Entity.Adapt<DeptDto>();
        }

        /// <summary>
        /// 删除系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteDept(int id)
        {
            var dept = await _deptRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }

            await _deptRepository.DeleteAsync(dept);
        }

        /// <summary>
        /// 更新系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DeptDto> UpdateDept(DeptDto deptDto)
        {
            var dept = await _deptRepository.FirstOrDefaultAsync(x => x.Id == deptDto.Id);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }
            var changeDept = deptDto.Adapt(dept);
             await changeDept.UpdateExcludeAsync(u=>u.CreateTime);
            return changeDept.Adapt<DeptDto>();
        }

        /// <summary>
        /// 查找系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DeptDto> FindDeptById(int id)
        {
            var dept = await _deptRepository
                .Entities
                .Select(u => new TbDept
                {
                    Id = u.Id,
                    DeptName = u.DeptName,
                    CreateTime = u.CreateTime,
                    Classes = u.Classes.Select(c => new TbClass
                    {
                        Id = c.Id,
                        ClassesName = c.ClassesName,
                        CreateTime = c.CreateTime,
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }

            return dept.Adapt<DeptDto>();
        }
        
        /// <summary>
        /// 根据教师查询系别分页列表
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<DeptDto>> FindDeptByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10)
        {
            var teacher = await _teacherRepository
                .Entities.AsNoTracking()
                .Where(u => u.Id == teacherId)
                .Select(u=>new TbTeacher{Id = u.Id})
                .FirstOrDefaultAsync();
            
            var pageResult = await _deptRepository
                .Entities.AsNoTracking()
                .Where(u=>Equals(u.Id, teacher.DeptId))
                .Select(u => new TbDept
                {
                    Id = u.Id,
                    DeptName = u.DeptName,
                    CreateTime = u.CreateTime,
                    Classes = u.Classes.Select(c => new TbClass
                    {
                        Id = c.Id,
                        ClassesName = c.ClassesName,
                        CreateTime = c.CreateTime,
                    }).ToList()
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<DeptDto>>();
        }

    }
}
