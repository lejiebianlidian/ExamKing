using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.ErrorCodes;
using Fur.DatabaseAccessor;
using Fur.DataEncryption;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 管理员服务
    /// </summary>
    public class ManageService : IManageService, ITransient
    {
        /// <summary>
        /// 管理员仓储
        /// </summary>
        private readonly IRepository<TbAdmin> _adminRepository;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManageService(IRepository<TbAdmin> adminRepository)
        {
            _adminRepository = adminRepository;
        }


        /// <summary>
        /// 分页查询管理员
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<AdminDto>> FindAdminAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _adminRepository.AsQueryable(false)
                .ProjectToType<AdminDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AdminDto> LoginAdmin(AdminDto adminDto)
        {
            var admin = await _adminRepository
                .Where(
                    u => u.Username.Equals(adminDto.Username))
                .SingleOrDefaultAsync();
            if (admin == null) throw Oops.Oh(AdminErrorCodes.a1002);
            if (!MD5Encryption.Compare(adminDto.Password, admin.Password))
            {
                throw Oops.Oh(AdminErrorCodes.a1001);
            }
            return admin.Adapt<AdminDto>();
        }

        /// <summary>
        /// 管理员注册
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        public async Task<AdminDto> CreateAdmin(AdminDto adminDto)
        {
            var admin = await _adminRepository.Entities.SingleOrDefaultAsync(u => u.Username.Equals(adminDto.Username));
            if (admin != null)
            {
                throw Oops.Oh(AdminErrorCodes.a1003);
            }
            
            var regAdmin = await _adminRepository.InsertNowAsync(adminDto.Adapt<TbAdmin>());
            return regAdmin.Entity.Adapt<AdminDto>();
        }

        /// <summary>
        /// 管理员更新
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AdminDto> UpdateAdmin(AdminDto adminDto)
        {
            var admin = await _adminRepository.Entities.SingleOrDefaultAsync(u => u.Id == adminDto.Id);
            if (admin == null)
            {
                throw Oops.Oh(AdminErrorCodes.a1002);
            }
            var newAdminEntity = adminDto.Adapt(admin);
            var newAdmin = await _adminRepository.UpdateAsync(newAdminEntity);
            return newAdminEntity.Adapt<AdminDto>();
        }

        /// <summary>
        /// 管理员删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteAdminById(int id)
        {
            var admin = await _adminRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (admin == null)
            {
                throw Oops.Oh(AdminErrorCodes.a1002);
            }
            await _adminRepository.DeleteAsync(admin);
        }

        /// <summary>
        /// 查找管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AdminDto> FindAdminById(int id)
        {
            var admin = await _adminRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (admin == null)
            {
                throw Oops.Oh(AdminErrorCodes.a1002);
            }

            return admin.Adapt<AdminDto>();
        }
    }
}