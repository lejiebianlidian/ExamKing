using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.DatabaseAccessor;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 教师接口
    /// </summary>
    public class TeacherController : ApiControllerBase
    {
        private readonly ITeacherService _teacherService;
        /// <summary>
        /// 依赖注入
        /// </summary>
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// 教师列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<TeacherInfoDto>> GetTeacherList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var teacherList = await _teacherService.FindTeacherAllByPage(
                pageIndex, pageSize);
            return teacherList.Adapt<PagedList<TeacherInfoDto>>();
        }
        
        /// <summary>
        /// 创建教师
        /// </summary>
        /// <param name="addTeacherInput"></param>
        /// <returns></returns>
        public async Task PostCreateTeacher(AddTeacherInput addTeacherInput)
        {
            await _teacherService.CreateTeacher(addTeacherInput.Adapt<TeacherDto>());
        }

        /// <summary>
        /// 修改教师
        /// </summary>
        /// <param name="editTeacherInput"></param>
        /// <returns></returns>
        public async Task UpdateEditTeacher(EditTeacherInput editTeacherInput)
        {
            await _teacherService.UpdateTeacher(editTeacherInput.Adapt<TeacherDto>());
        }

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRemoveTeacher(int id)
        {
            await _teacherService.DeleteTeacher(id);
        }
    }
}