using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Teacher
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
        public async Task<PagedList<DeptOutput>> GetDeptList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var teacherId = GetUserId();
            var depts=await _deptService.FindDeptByTeacherAndPage(teacherId, pageIndex, pageSize);
            return depts.Adapt<PagedList<DeptOutput>>();
        }
        
    }
}
