using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using ExamKing.Core.Utils;
using Fur;
using Fur.Authorization;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc.Core;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 学生接口
    /// </summary>
    public class MemberController : ApiControllerBase
    {
        
        private readonly IStudentService _studentService;

        /// <inheritdoc />
        public MemberController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 学生登录接口
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [UnifyResult(typeof(LoginOutput))]
        public async Task<LoginOutput> PostLogin(LoginInput loginInput)
        {
            var student = await _studentService.Login(loginInput.StudentNo, loginInput.Password);
            var output = student.Adapt<LoginOutput>();
            
            // 生成 token
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();
            var datetimeOffset = new DateTimeOffset(DateTime.Now);
            output.AccessToken = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new JObject()
            {
                { "UserId", student.Id },  // 存储Id
                { JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddSeconds(jwtSettings.ExpiredTime.Value*60)).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                { JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
            });

            return output;
        }

        /// <summary>
        /// 学生注册接口
        /// </summary>
        /// <param name="resgisterInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [UnifyResult(typeof(ResgisterOutput))]
        public async Task<ResgisterOutput> PostRegister(ResgisterInput resgisterInput)
        {
            var student = await _studentService.Register(resgisterInput.Adapt<StudentDto>());
            return student.Adapt<ResgisterOutput>();
        }
        
        /// <summary>
        /// 学生信息
        /// </summary>
        /// <returns></returns>
        [UnifyResult(typeof(StudentDto))]
        public async Task<StudentDto> GetInfo(int Id)
        {
            var studentInfo = await _studentService.GetInfoById(Id);
            return studentInfo;
        }
    }
}