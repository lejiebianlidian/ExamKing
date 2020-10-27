using Fur.DatabaseAccessor;
using System.Threading.Tasks;
using ExamKing.Core.Entites;

using ExamKing.Application.Mappers;
using Mapster;
using Fur.DependencyInjection;
using System;

namespace ExamKing.Application.Services
{
    public class DeptService : IDeptService, ITransient
    {

        private readonly IRepository<TbDept> _deptRepository;
        public DeptService(IRepository<TbDept> deptRepository)
        {
            _deptRepository = deptRepository;
        }

        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public async Task<TbDept> InsertDept(DeptDto deptDto)
        {

            var dept = await _deptRepository.InsertNowAsync(deptDto.Adapt<TbDept>());
            return dept.Entity;
        }
    }
}
