using System;
using System.Collections.Generic;
using System.Linq;
using Furion.DatabaseAccessor;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;
using ExamKing.Application.ErrorCodes;
using ExamKing.Core.Utils;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 班级服务
    /// </summary>
    public class ClassesService : IClassesService, ITransient
    {
        private readonly IRepository<TbClass> _classRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="classRepository"></param>
        public ClassesService(IRepository<TbClass> classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// 分页查询班级
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ClassesDto>> FindClassesAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _classRepository
                .Entities.AsNoTracking()
                .Select(u => new TbClass
                {
                    Id=u.Id,
                    ClassesName=u.ClassesName,
                    CreateTime=u.CreateTime,
                    DeptId = u.DeptId,
                    Dept = new TbDept
                    {
                        CreateTime=u.Dept.CreateTime,
                        DeptName=u.Dept.DeptName
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return  pageResult.Adapt<PagedList<ClassesDto>>();
        }

        /// <summary>
        /// 新增班级    
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public async Task<ClassesDto> InsertClasses(ClassesDto classesDto)
        {
            // 判断系别是否存在
            var dept = await _classRepository.Change<TbDept>()
                .FirstOrDefaultAsync(x => x.Id == classesDto.DeptId);
            if (dept == null) throw Oops.Oh(DeptErrorCodes.d1301);
            // classesDto.CreateTime = TimeUtil.GetTimeStampNow();
            var classes = await _classRepository.InsertNowAsync(classesDto.Adapt<TbClass>());
            return classes.Entity.Adapt<ClassesDto>();
        }

        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteClasses(int id)
        {
            var classes = await _classRepository
                .FirstOrDefaultAsync(x => x.Id == id);
            if (classes==null)
            {
                throw Oops.Oh(ClassErrorCodes.c1101);
            }

            await _classRepository.DeleteAsync(classes);
        }

        /// <summary>
        /// 更新班级
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ClassesDto> UpdateClasses(ClassesDto classesDto)
        {
            var classes = await _classRepository
                .FirstOrDefaultAsync(x => x.Id == classesDto.Id);
            if (classes==null)
            {
                throw Oops.Oh(ClassErrorCodes.c1101);
            }

            var changeClasses = classesDto.Adapt(classes);
            await changeClasses
                .UpdateExcludeAsync(u => u.CreateTime);
            return changeClasses.Adapt<ClassesDto>();
        }

        /// <summary>
        /// 查找班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ClassesDto> FindClassesById(int id)
        {
            var classes = await _classRepository
                .Include(x=>x.Dept)
                .Select(u => new TbClass
                {
                    Id=u.Id,
                    ClassesName=u.ClassesName,
                    CreateTime=u.CreateTime,
                    DeptId = u.DeptId,
                    Dept = new TbDept
                    {
                        DeptName=u.Dept.DeptName,
                    }
                }).FirstOrDefaultAsync(x => x.Id == id);
            if (classes==null)
            {
                throw Oops.Oh(ClassErrorCodes.c1101);
            }

            return classes.Adapt<ClassesDto>();
        }
    }
}
