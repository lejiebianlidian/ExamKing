using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 选择题服务
    /// </summary>
    public interface ISelectService
    {
        /// <summary>
        /// 选择题创建
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public Task<SelectDto> CreateSelect(SelectDto selectDto);

        /// <summary>
        /// 选择题更新
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public Task<SelectDto> UpdateSelect(SelectDto selectDto);

        /// <summary>
        /// 选择题删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteSelect(int id);

        /// <summary>
        /// 根据教师查询选择题分页
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<SelectDto>> FindSelectAllByTeacherAndPage(int teacherId, bool isSingle, int pageIndex = 1,
            int pageSize = 10);


        /// <summary>
        /// 根据id查询选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SelectDto> FindSelectById(int id);

    }
}