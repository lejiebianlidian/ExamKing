using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using System.Threading.Tasks;
using ExamKing.Core.Entites;

using ExamKing.Application.Mappers;
using ExamKing.Core.ErrorCodes;
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
        
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="deptRepository"></param>
        public DeptService(IRepository<TbDept> deptRepository)
        {
            _deptRepository = deptRepository;
        }

        /// <summary>
        /// 查询全部系别和班级
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptDto>> FindDeptAll()
        {
            var depts = _deptRepository
                .AsQueryable(false)
                .Include(x=>x.TbClasses)
                .ProjectToType<DeptDto>();
            return await depts.ToListAsync();
        }

        /// <summary>
        /// 分页查询系别
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<DeptDto>> FindDeptAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _deptRepository.AsQueryable(false)
                .ProjectToType<DeptDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
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

        /// <summary>
        /// 删除系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteDept(int id)
        {
            var dept = await _deptRepository.SingleOrDefaultAsync(x => x.Id == id);

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
            var dept = await _deptRepository.SingleOrDefaultAsync(x => x.Id == deptDto.Id);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }
            
            var changeDept = await _deptRepository.UpdateNowAsync(deptDto.Adapt(dept));
            return changeDept.Entity.Adapt<DeptDto>();
        }

        /// <summary>
        /// 查找系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DeptDto> FindDeptById(int id)
        {
            var dept = await _deptRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (dept == null)
            {
                throw Oops.Oh(DeptErrorCodes.d1301);
            }

            return dept.Adapt<DeptDto>();
        }
    }
}
