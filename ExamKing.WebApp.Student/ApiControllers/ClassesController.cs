using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using ExamKing.Application.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 班级接口
    /// </summary>
    public class ClassesController : ApiControllerBase
    {
        private readonly IClassesService _classesService;

        /// <inheritdoc />
        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }

        /// <summary>
        /// 查询全部班级
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<ClassesDto>> GetClassesAll()
        {
            return await _classesService.FindClassesAll();
        }
        
        /// <summary>
        /// 根据系别查询班级
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<ClassesDto>> GetClassesByDept(int detpId)
        {
            return await _classesService.FindClassessByDeptId(detpId);
        }
    }
}