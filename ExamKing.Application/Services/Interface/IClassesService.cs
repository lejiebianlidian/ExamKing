using System.Collections.Generic;
using ExamKing.Application.Mappers;
using System.Threading.Tasks;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 班级服务
    /// </summary>
    public interface IClassesService
    {
        /// <summary>
        /// 查询全部班级
        /// </summary>
        /// <returns></returns>
        public Task<List<ClassesDto>> FindClassesAll();

        /// <summary>
        /// 根据系别查询班级
        /// </summary>
        /// <returns></returns>
        public Task<List<ClassesDto>> FindClassessByDeptId(int detpId);
        
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="classesDto"></param>
        /// <returns></returns>
        public Task<ClassesDto> InsertClass(ClassesDto classesDto);
    }
}
