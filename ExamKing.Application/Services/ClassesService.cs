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
        /// 查询全部班级
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClassesDto>> FindClassesAll()
        {
            var classes = _classRepository
                .AsQueryable()
                .ProjectToType<ClassesDto>();
            return await classes.ToListAsync();
        }

        /// <summary>
        /// 新增班级    
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public async Task<ClassesDto> InsertClass(ClassesDto classesDto)
        {
            // 判断系别是否存在
            var dept = await _classRepository.Change<TbDept>().AnyAsync(x => x.Id == classesDto.Deptld);
            if (dept == false) throw Oops.Oh(ClassErrorCodes.c1000);
            var classes = await _classRepository.InsertNowAsync(classesDto.Adapt<TbClass>());
            return classes.Entity.Adapt<ClassesDto>();
        }
    }
}
