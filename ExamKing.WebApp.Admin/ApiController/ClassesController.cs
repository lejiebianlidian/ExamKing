using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace ExamKing.WebApp.Admin
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
        /// 班级列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ClassesDeptOutput>> GetClassesList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var list = await _classesService.FindClassesAllByPage(pageIndex, pageSize);
            return list.Adapt<PagedList<ClassesDeptOutput>>();
        }
        
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="addClassInput"></param>
        /// <returns></returns>
        public async Task<AddClassOutput> InsertAddClasses(AddClassInput addClassInput)
        {
            var classes = await _classesService.InsertClasses(addClassInput.Adapt<ClassesDto>());
            return classes.Adapt<AddClassOutput>();
        }

        /// <summary>
        /// 修改班级
        /// </summary>
        /// <param name="editClassesInput"></param>
        /// <returns></returns>
        public async Task<ClassesOutput> UpdateEditClasses(EditClassesInput editClassesInput)
        {
            var changeClasses = await _classesService.UpdateClasses(
                editClassesInput.Adapt<ClassesDto>());
            return changeClasses.Adapt<ClassesOutput>();
        }
        
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveClasses(int id)
        {
            await _classesService.DeleteClasses(id);
            return "success";
        }

        /// <summary>
        /// 查询班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClassesDeptOutput> GetFindClasses(int id)
        {
            var classes = await _classesService.FindClassesById(id);
            return classes.Adapt<ClassesDeptOutput>();
        }
    }
}
