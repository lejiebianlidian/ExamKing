using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Furion.DatabaseAccessor;
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
        /// 查询全部系别
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptOutput>> GetDeptAll()
        {
            var depts= await _deptService.FindDeptAll();
            return depts.Adapt<List<DeptOutput>>();
        }
        
        /// <summary>
        /// 系别列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<DeptOutput>> GetDeptList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var depts=await _deptService.FindDeptAllByPage(pageIndex, pageSize);
            return depts.Adapt<PagedList<DeptOutput>>();
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
        public async Task<DeptOutput> UpdateEditDept(EditDeptInput editDeptInput)
        {
            var changeDept = await _deptService.UpdateDept(editDeptInput.Adapt<DeptDto>());
            return changeDept.Adapt<DeptOutput>();
        }
        
        /// <summary>
        /// 删除系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveDept(int id)
        {
            await _deptService.DeleteDept(id);
            
            return "success";
        }
        
        /// <summary>
        /// 查询系别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeptOutput> GetFindDept(int id)
        {
            var dept = await _deptService.FindDeptById(id);
            return dept.Adapt<DeptOutput>();
        }
    }
}
