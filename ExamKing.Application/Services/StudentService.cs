using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Core.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Fur.DatabaseAccessor;
using Fur.DataEncryption;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 学生服务
    /// </summary>
    public class StudentService : IStudentService, ITransient
    {
        private readonly IRepository<TbStudent> _studentRepository;
        
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="studentRepository"></param>
        public StudentService(IRepository<TbStudent> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// 分页查询学生
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<StudentDto>> FindStudentAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _studentRepository.Entities.AsNoTracking()
                .Select(u=>new TbStudent
                {
                    Id=u.Id,
                    StuName=u.StuName,
                    DeptId=u.DeptId,
                    ClassesId=u.ClassesId,
                    Sex=u.Sex,
                    StuNo=u.StuNo,
                    Telphone=u.Telphone,
                    IdCard=u.IdCard,
                    CreateTime=u.CreateTime,
                    Classes = new TbClass
                    {
                        Id=u.Classes.Id,
                        ClassesName=u.Classes.ClassesName,
                        CreateTime=u.Classes.CreateTime,
                        DeptId = u.Classes.DeptId,
                        Dept = new TbDept
                        {
                            CreateTime=u.Classes.Dept.CreateTime,
                            DeptName=u.Classes.Dept.DeptName
                        }
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<StudentDto>>();
        }

        /// <summary>
        /// 学生登录
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<StudentDto> LoginStudent(string studentNo, string password)
        {
            var student = await _studentRepository
                .FirstOrDefaultAsync(s => s.StuNo.Equals(studentNo));
            if (student == null) throw Oops.Oh(StudentErrorCodes.s1204);
            if (!MD5Encryption.Compare(password, student.Password))
            {
                throw Oops.Oh(StudentErrorCodes.s1201);
            }
            return student.Adapt<StudentDto>();
        }


        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public async Task<StudentDto> RegisterStudent(StudentDto studentDto)
        {
            // 判断学号是否已经注册
            var stu = await _studentRepository.Where(x => x.StuNo == studentDto.StuNo).FirstOrDefaultAsync();
            if (stu != null) throw Oops.Oh(StudentErrorCodes.s1205);
            // 判断班级是否存在
            var classes = await _studentRepository.Change<TbClass>().FirstOrDefaultAsync(x => x.Id == studentDto.ClassesId);
            if (classes == null) throw Oops.Oh(StudentErrorCodes.s1202);
            // 判断班级是否属于该系别
            if (classes.DeptId != studentDto.DeptId) throw Oops.Oh(StudentErrorCodes.s1203);
            // studentDto.CreateTime = TimeUtil.GetTimeStampNow();
            var stduent = await _studentRepository
                .InsertNowAsync(studentDto.Adapt<TbStudent>());
            return stduent.Entity.Adapt<StudentDto>();
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDto> FindStudentById(int id)
        {
            var student = await _studentRepository
                .Include(x=>x.Classes.Dept)
                .Select(u=>new TbStudent
                {
                    Id=u.Id,
                    StuName=u.StuName,
                    DeptId=u.DeptId,
                    ClassesId=u.ClassesId,
                    Sex=u.Sex,
                    StuNo=u.StuNo,
                    Telphone=u.Telphone,
                    IdCard=u.IdCard,
                    CreateTime=u.CreateTime,
                    Classes = new TbClass
                    {
                        Id=u.Classes.Id,
                        ClassesName=u.Classes.ClassesName,
                        CreateTime=u.Classes.CreateTime,
                        DeptId = u.Classes.DeptId,
                        Dept = new TbDept
                        {
                            CreateTime=u.Classes.Dept.CreateTime,
                            DeptName=u.Classes.Dept.DeptName
                        }
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) throw Oops.Oh(StudentErrorCodes.s1204);
            return student.Adapt<StudentDto>();
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDto> UpdateStudent(StudentDto studentDto)
        {
            var stu = await _studentRepository
                .Entities
                .Include(x=>x.Classes)
                .Include(x=>x.Dept)
                .FirstOrDefaultAsync(x => x.Id == studentDto.Id);            
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);
            var newStu= studentDto.Adapt(stu);
            var changeInfo = await newStu.UpdateExcludeAsync(u => u.CreateTime);
            return newStu.Adapt<StudentDto>();
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="stuNo"></param>
        /// <param name="idCard"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDto> ForgetPass(string stuNo, string idCard, string newPass)
        {
            var stu = await _studentRepository.Where(x => x.StuNo == stuNo).FirstOrDefaultAsync();
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);
            // 判断身份证后6位是否正确
            if (idCard != stu.IdCard.Substring(stu.IdCard.Length-6,6)) throw Oops.Oh(StudentErrorCodes.s1206);
            // 修改密码
            stu.Password = MD5Encryption.Encrypt(newPass);
            var newStu = await _studentRepository.UpdateAsync(stu);
            return newStu.Entity.Adapt<StudentDto>();
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteStudent(int id)
        {
            var stu = await _studentRepository.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);

            await _studentRepository.DeleteAsync(stu);
        }

    }
}