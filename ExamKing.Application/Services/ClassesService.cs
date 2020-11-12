using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;
using ExamKing.Core.ErrorCodes;
using Fur.DependencyInjection;
using Fur.FriendlyException;
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
            var pageResult = _classRepository.Entities.AsNoTracking()
                .ProjectToType<ClassesDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 新增班级    
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public async Task<ClassesDto> InsertClasses(ClassesDto classesDto)
        {
            // 判断系别是否存在
            var dept = await _classRepository.Change<TbDept>().SingleOrDefaultAsync(x => x.Id == classesDto.DeptId);
            if (dept == null) throw Oops.Oh(DeptErrorCodes.d1301);
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
            var classes = await _classRepository.SingleOrDefaultAsync(x => x.Id == id);
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
            var classes = await _classRepository.SingleOrDefaultAsync(x => x.Id == classesDto.Id);
            if (classes==null)
            {
                throw Oops.Oh(ClassErrorCodes.c1101);
            }

            var changeClasses = await _classRepository.UpdateNowAsync(classesDto.Adapt(classes));
            return changeClasses.Entity.Adapt<ClassesDto>();
        }

        /// <summary>
        /// 查找班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ClassesDto> FindClassesById(int id)
        {
            var classes = await _classRepository.Include(x=>x.Dept).SingleOrDefaultAsync(x => x.Id == id);
            if (classes==null)
            {
                throw Oops.Oh(ClassErrorCodes.c1101);
            }

            return classes.Adapt<ClassesDto>();
        }
    }
}
