using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Mapster;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 系别接口
    /// </summary>
    public class DeptController : ApiControllerBase
    {
        private readonly IDeptService _deptService;
        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptName">系别名称</param>
        /// <returns>新增系别Id</returns>
        public async Task<AddDeptOutput> InsertAddDept(AddDeptInput addDeptInput)
        {
            var dept = await _deptService.InsertDept(addDeptInput.Adapt<DeptDto>());
            return dept.Adapt<AddDeptOutput
                >();
        }
    }
}
