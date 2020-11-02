using System.Collections.Generic;
using Fur.DatabaseAccessor;
using System.Threading.Tasks;
using ExamKing.Core.Entites;

using ExamKing.Application.Mappers;
using Mapster;
using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 系别服务
    /// </summary>
    public class DeptService : IDeptService, ITransient
    {

        private readonly IRepository<TbDept> _deptRepository;
        
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="deptRepository"></param>
        public DeptService(IRepository<TbDept> deptRepository)
        {
            _deptRepository = deptRepository;
        }

        /// <summary>
        /// 查询全部系别
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptDto>> FindDeptAll()
        {
            var depts = _deptRepository.AsQueryable()
                .ProjectToType<DeptDto>();
            return await depts.ToListAsync();
        }

        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public async Task<DeptDto> InsertDept(DeptDto deptDto)
        {
            var dept = await _deptRepository.InsertNowAsync(deptDto.Adapt<TbDept>());
            return dept.Entity.Adapt<DeptDto>();
        }
    }
}
