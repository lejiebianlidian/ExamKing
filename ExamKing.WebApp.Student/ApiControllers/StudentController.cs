using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Fur;
using Fur.Authorization;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生接口
    /// </summary>
    public class StudentController : ApiControllerBase
    {
        
        private readonly IStudentService _studentService;

        /// <inheritdoc />
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 学生登录接口
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<LoginOutput> PostLogin(LoginInput loginInput)
        {
            var student = await _studentService.Login(loginInput.StudentNo, loginInput.Password);
            var output = student.Adapt<LoginOutput>();
            
            // 生成 token
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();
            var datetimeOffset = new DateTimeOffset(DateTime.Now);
            output.AccessToken = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new Dictionary<string, object>()
            {
                { "UserId", student.Id },  // 存储Id
                { JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddSeconds(jwtSettings.ExpiredTime.Value*60*60*24*30)).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                { JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
            });
            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.HttpContext.Response.Headers["access-token"] = output.AccessToken;

            return output;
        }

        /// <summary>
        /// 学生注册接口
        /// </summary>
        /// <param name="resgisterInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task PostRegister(ResgisterInput resgisterInput)
        {
            var student = await _studentService.Register(resgisterInput.Adapt<StudentDto>());
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="forgetPassDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task PostForgetPass(ForgetPassDto forgetPassDto)
        {
            await _studentService.ForgetPass(forgetPassDto.StuNo, forgetPassDto.IdCard, forgetPassDto.NewPass);
        }
        
        /// <summary>
        /// 学生信息
        /// </summary>
        /// <returns></returns>
        public async Task<StuInfoDto> GetInfo()
        {
            var userId = getUserId();
            var studentInfo = await _studentService.GetInfoById(userId);
            return studentInfo.Adapt<StuInfoDto>();
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="editStuInput"></param>
        /// <returns></returns>
        public async Task UpdateEditInfo(EditStuInput editStuInput)
        {
            var changeDto = editStuInput.Adapt<StudentDto>();
            changeDto.Id = getUserId();
            await _studentService.UpdateInfo(changeDto);
        }
    }
}