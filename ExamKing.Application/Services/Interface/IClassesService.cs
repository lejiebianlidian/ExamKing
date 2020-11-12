using System.Collections.Generic;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;
using ExamKing.Core.Entites;
using Fur.DatabaseAccessor;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 班级服务
    /// </summary>
    public interface IClassesService
    {

        /// <summary>
        /// 分页查询班级
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ClassesDto>> FindClassesAllByPage(int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public Task<ClassesDto> InsertClasses(ClassesDto classesDto);

        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteClasses(int id);

        /// <summary>
        /// 更新班级
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public Task<ClassesDto> UpdateClasses(ClassesDto classesDto);
        
        /// <summary>
        /// 查找班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ClassesDto> FindClassesById(int id);
    }
}
