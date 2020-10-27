using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 系别服务
    /// </summary>
    public interface IDeptService
    {
        /// <summary>
        /// 新增系别
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        public Task<TbDept> InsertDept(DeptDto deptDto);
    }
}
