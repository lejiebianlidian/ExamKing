using Fur.DatabaseAccessor;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using ExamKing.Application.Common;

namespace ExamKing.Application.Services
{
    public class ClassesService : IClassesService, ITransient
    {
        private readonly IRepository<TbClass> _classRepository;
        public ClassesService(IRepository<TbClass> classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// 新增班级    
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public async Task<TbClass> InsertClass(ClassesDto classesDto)
        {
            // 判断系别是否存在
            var dept = await _classRepository.Change<TbDept>().AnyAsync(x => x.Id == classesDto.Deptld);
            if (dept == false) throw Oops.Oh(ClassErrorCodes.c1000);
            classesDto.Dept = dept.Adapt<DeptDto>();
            var classes = await _classRepository.InsertNowAsync(classesDto.Adapt<TbClass>());
            return classes.Entity;
        }
    }
}
