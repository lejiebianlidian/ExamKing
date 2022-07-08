using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using Furion.DatabaseAccessor;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 管理员服务
    /// </summary>
    public interface IManageService
    {
        /// <summary>
        /// 分页查询管理员
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<AdminDto>> FindAdminAllByPage(int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <returns></returns>
        public Task<AdminDto> LoginAdmin(AdminDto adminDto);

        /// <summary>
        /// 管理员注册
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        public Task<AdminDto> CreateAdmin(AdminDto adminDto);

        /// <summary>
        /// 管理员更新
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        public Task<AdminDto> UpdateAdmin(AdminDto adminDto);

        /// <summary>
        /// 管理员删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAdminById(int id);

        /// <summary>
        /// 查找管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AdminDto> FindAdminById(int id);
    }
}