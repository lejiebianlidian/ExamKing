using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using Fur.DatabaseAccessor;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 系别服务
    /// </summary>
    public interface IDeptService
    {
        /// <summary>
        /// 查询全部系别
        /// </summary>
        /// <returns></returns>
        public Task<List<DeptDto>> FindDeptAll();
        
        /// <summary>
        /// 分页查询系别
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<DeptDto>> FindDeptAllByPage(int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public Task<DeptDto> InsertDept(DeptDto deptDto);

        /// <summary>
        /// 删除系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteDept(int id);

        /// <summary>
        /// 更新系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public Task<DeptDto> UpdateDept(DeptDto deptDto);
        
    }
}
