using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur;
using Fur.Authorization;
using Fur.DataEncryption;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员接口
    /// </summary>
    public class ManageController : ApiControllerBase
    {
        private readonly IManageService _manageService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManageController(IManageService manageService)
        {
            _manageService = manageService;
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginAdminInput"></param>
        /// <returns></returns>
        [AllowAnonymous]       
        public async Task<LoginAdminOutput> PostLogin(LoginAdminInput loginAdminInput)
        {
            var admin = await _manageService.LoginAdmin(loginAdminInput.Adapt<AdminDto>());
            var output = admin.Adapt<LoginAdminOutput>();
            // 生成 token
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();
            var datetimeOffset = new DateTimeOffset(DateTime.Now);
            if (jwtSettings.ExpiredTime != null)
                output.AccessToken = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey,
                    new Dictionary<string, object>()
                    {
                        {"UserId", admin.Id}, // 存储Id
                        {"IsAdmin", true}, // 管理员登录 
                        {JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds()},
                        {JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds()},
                        {
                            JwtRegisteredClaimNames.Exp,
                            new DateTimeOffset(
                                    DateTime.Now.AddSeconds(
                                        jwtSettings.ExpiredTime.Value * 60 * 60 * 24 * 30))
                                .ToUnixTimeSeconds()
                        },
                        {JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                        {JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience}
                    });
            // 设置 Swagger 刷新自动授权
            if (_httpContextAccessor.HttpContext != null)
                _httpContextAccessor.HttpContext.Response.Headers["access-token"] = output.AccessToken;

            return output;
        }

        /// <summary>
        /// 注册管理员
        /// </summary>
        /// <param name="registerAdminInput"></param>
        /// <returns></returns>
        public async Task PostRegister(RegisterAdminInput registerAdminInput)
        {
            await _manageService.CreateAdmin(registerAdminInput.Adapt<AdminDto>());
        }

        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="editAdminInput"></param>
        /// <returns></returns>
        public async Task<AdminInfoDto> UpdateEditInfo(EditAdminInput editAdminInput)
        {
            var adminDto = editAdminInput.Adapt<AdminDto>();
            adminDto.Id = GetUserId();
            var admin = await _manageService.UpdateAdmin(adminDto);
            return admin.Adapt<AdminInfoDto>();
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRemoveAdmin(int id)
        {
            await _manageService.DeleteAdminById(id);
        }

        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <returns></returns>
        public async Task<AdminInfoDto> GetInfo()
        {
            var adminId = GetUserId();
            var admin = await _manageService.FindAdminById(adminId);
            return admin.Adapt<AdminInfoDto>();
        }
    }
}