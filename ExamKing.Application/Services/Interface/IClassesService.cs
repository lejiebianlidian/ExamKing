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
        public Task<TbClass> InsertClass(ClassesDto classesDto);
    }
}
