using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.DatabaseAccessor;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 系别接口
    /// </summary>
    public class DeptController : ApiControllerBase
    {
        private readonly IDeptService _deptService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="deptService"></param>
        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        /// <summary>
        /// 系别列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<DeptDto>> GetDeptList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            return await _deptService.FindDeptAllByPage(pageIndex, pageSize);
        }
        
        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="addDeptInput"></param>
        /// <returns>新增系别Id</returns>
        public async Task<AddDeptOutput> InsertAddDept(AddDeptInput addDeptInput)
        {
            var dept = await _deptService.InsertDept(addDeptInput.Adapt<DeptDto>());
            return dept.Adapt<AddDeptOutput
                >();
        }

        /// <summary>
        /// 修改系别
        /// </summary>
        /// <param name="editDeptInput"></param>
        /// <returns></returns>
        public async Task<DeptDto> UpdateEditDept(EditDeptInput editDeptInput)
        {
            var changeDept = await _deptService.UpdateDept(editDeptInput.Adapt<DeptDto>());
            return changeDept;
        }
        
        /// <summary>
        /// 删除系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRemoveDept(int id)
        {
            await _deptService.DeleteDept(id);
        }
    }
}
