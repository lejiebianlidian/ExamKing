using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur.DatabaseAccessor;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 学生接口
    /// </summary>
    public class StudentController : ApiControllerBase
    {
        private readonly IStudentService _studentService;
        /// <summary>
        /// 依赖注入
        /// </summary>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 学生列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<StudentClassesOutput>> GetStudentList(
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10)
        {
            var stuList = await _studentService.FindStudentAllByPage(pageIndex, pageSize);
            return stuList.Adapt<PagedList<StudentClassesOutput>>();
        }
        
        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="addStudentInput"></param>
        /// <returns></returns>
        public async Task<StudentInfoOutput> PostCreateStudent(AddStudentInput addStudentInput)
        {
            var stu = await _studentService.RegisterStudent(addStudentInput.Adapt<StudentDto
            >());
            return stu.Adapt<StudentInfoOutput>();
        }

        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="editStudentInput"></param>
        /// <returns></returns>
        public async Task<string> UpdateEditStudent(EditStudentInput editStudentInput)
        { 
            await _studentService.UpdateStudent(editStudentInput
                .Adapt<StudentDto>());
            return "success";
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveStudent(int id)
        {
            await _studentService.DeleteStudent(id);
            return "success";
        }
        
        /// <summary>
        /// 查询学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentClassesOutput> GetFindStudent(int id)
        {
            var stu = await _studentService.FindStudentById(id);
            return stu.Adapt<StudentClassesOutput>();
        }
    }
}