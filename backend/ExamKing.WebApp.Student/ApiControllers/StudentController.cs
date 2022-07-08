using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Furion;
using Furion.Authorization;
using Furion.DataEncryption;
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
            var student = await _studentService.LoginStudent(loginInput.StudentNo, loginInput.Password);
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
        public async Task<string> PostRegister(ResgisterInput resgisterInput)
        {
            var student = await _studentService.RegisterStudent(resgisterInput.Adapt<StudentDto>());
            return "success";
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="forgetPassDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<string> PostForgetPass(ForgetPassInput forgetPassInput)
        {
            await _studentService.ForgetPass(forgetPassInput.StuNo, forgetPassInput.IdCard, forgetPassInput.NewPass);
            return "success";
        }
        
        /// <summary>
        /// 学生信息
        /// </summary>
        /// <returns></returns>
        public async Task<StudentOutput> GetInfo()
        {
            var student = await GetStudent();
            var studentInfo = await _studentService.FindStudentById(student.Id);
            return studentInfo.Adapt<StudentOutput>();
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="editStuInput"></param>
        /// <returns></returns>
        public async Task<string> UpdateEditInfo(EditStudentInput editStuInput)
        {
            var student = await GetStudent();
            var changeDto = editStuInput.Adapt<StudentDto>();
            changeDto.Id = student.Id;
            await _studentService.UpdateStudent(changeDto);
            return "success";
        }
    }
}