using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Core.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
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
            var pageResult = _studentRepository.AsQueryable(false)
                .ProjectToType<StudentDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 学生登录
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<StudentDto> LoginStudent(string studentNo, string password)
        {
            var student = await _studentRepository.SingleOrDefaultAsync(s => s.StuNo.Equals(studentNo) && s.Password.Equals(password));
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
            var stu = await _studentRepository.Where(x => x.StuNo == studentDto.StuNo).SingleOrDefaultAsync();
            if (stu != null) throw Oops.Oh(StudentErrorCodes.s1205);
            // 判断班级是否存在
            var classes = await _studentRepository.Change<TbClass>().SingleOrDefaultAsync(x => x.Id == studentDto.ClassesId);
            if (classes == null) throw Oops.Oh(StudentErrorCodes.s1202);
            // 判断班级是否属于该系别
            if (classes.DeptId != studentDto.DeptId) throw Oops.Oh(StudentErrorCodes.s1203);
            var stduent = await _studentRepository.InsertNowAsync(studentDto.Adapt<TbStudent>());
            return stduent.Entity.Adapt<StudentDto>();
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDto> GetStudentById(int id)
        {
            var student = await _studentRepository
                .Entities
                .Include(x =>x.Classes)
                .Include(x=>x.Dept)
                .SingleOrDefaultAsync(x => x.Id == id);
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
                .SingleOrDefaultAsync(x => x.Id == studentDto.Id);            
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);
            var newStu= studentDto.Adapt(stu);
            var changeInfo = await _studentRepository.UpdateAsync(stu);
            return changeInfo.Adapt<StudentDto>();
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
            var stu = await _studentRepository.Where(x => x.StuNo == stuNo).SingleOrDefaultAsync();
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);
            // 判断身份证后6位是否正确
            if (idCard != stu.IdCard.Substring(stu.IdCard.Length-6,6)) throw Oops.Oh(StudentErrorCodes.s1206);
            // 修改密码
            stu.Password = newPass;
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
            var stu = await _studentRepository.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);

            await _studentRepository.DeleteAsync(stu);
        }

        /// <summary>
        /// 查找学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StudentDto> FindStudentById(int id)
        {
            var stu = await _studentRepository.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (stu == null) throw Oops.Oh(StudentErrorCodes.s1204);

            return stu.Adapt<StudentDto>();
        }
    }
}