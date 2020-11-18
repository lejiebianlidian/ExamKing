using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.ErrorCodes;
using ExamKing.Core.Utils;
using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Mapster;
using System.Linq;
using Fur.DataEncryption;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 教师服务
    /// </summary>
    public class TeacherService : ITeacherService, ITransient
    {
        /// <summary>
        /// 教师仓储
        /// </summary>
        private readonly IRepository<TbTeacher> _teacherRepository;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public TeacherService(IRepository<TbTeacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// 分页查询教师
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<TeacherDto>> FindTeacherAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _teacherRepository
                .Entities.AsNoTracking()
                .Select(u => new TbTeacher
                {
                    Id=u.Id,
                    TeacherName=u.TeacherName,
                    DeptId=u.DeptId,
                    Sex=u.Sex,
                    TeacherNo=u.TeacherNo,
                    Telphone=u.Telphone,
                    IdCard=u.IdCard,
                    CreateTime=u.CreateTime,
                    Dept = new TbDept
                    {
                        CreateTime=u.Dept.CreateTime,
                        DeptName=u.Dept.DeptName
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return  pageResult.Adapt<PagedList<TeacherDto>>();
        }

        /// <summary>
        /// 创建教师
        /// </summary>
        /// <param name="teacherDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TeacherDto> CreateTeacher(TeacherDto teacherDto)
        {
            // 查询系别是否存在
            var dept = await _teacherRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(u => u.Id == teacherDto.DeptId);
            if (dept==null)
            {
                throw Oops.Oh(DeptErrorCodes
                    .d1301);
            }
            var teacher = await _teacherRepository.Entities.SingleOrDefaultAsync(u => u.TeacherNo.Equals(teacherDto.TeacherNo));
            if (teacher != null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1401);
            }
            // teacherDto.CreateTime = TimeUtil.GetTimeStampNow();
            var createTeacher = await _teacherRepository.InsertNowAsync(teacherDto.Adapt<TbTeacher>());
            return createTeacher.Entity.Adapt<TeacherDto>();
        }

        /// <summary>
        /// 更新教师
        /// </summary>
        /// <param name="teacherDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TeacherDto> UpdateTeacher(TeacherDto teacherDto)
        {
            var teacher = await _teacherRepository.Entities.SingleOrDefaultAsync(u => u.Id == teacherDto.Id);
            if (teacher == null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }
            // 查询系别是否存在
            var dept = await _teacherRepository.Change<TbDept>().Entities
                .SingleOrDefaultAsync(u => u.Id == teacher.DeptId);
            if (dept==null)
            {
                throw Oops.Oh(DeptErrorCodes
                    .d1301);
            }
            var changeTeacher = await _teacherRepository.UpdateNowAsync(teacherDto.Adapt(teacher));
            return changeTeacher.Entity.Adapt<TeacherDto>();
        }

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.SingleOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }
            await _teacherRepository.DeleteAsync(teacher);
        }

        /// <summary>
        /// 查询教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TeacherDto> FindTeacherById(int id)
        {
            var teacher = await _teacherRepository
                .Entities
                .Select(u => new TbTeacher
                {
                    Id=u.Id,
                    TeacherName=u.TeacherName,
                    DeptId=u.DeptId,
                    Sex=u.Sex,
                    TeacherNo=u.TeacherNo,
                    Telphone=u.Telphone,
                    IdCard=u.IdCard,
                    CreateTime=u.CreateTime,
                    Dept = new TbDept
                    {
                        CreateTime=u.Dept.CreateTime,
                        DeptName=u.Dept.DeptName
                    }
                })
                .SingleOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                throw Oops.Oh(TeacherErrorCodes.t1402);
            }

            return teacher.Adapt<TeacherDto>();
        }

        /// <summary>
        /// 教师登录
        /// </summary>
        /// <param name="teacherNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TeacherDto> LoginTeacher(string teacherNo, string password)
        {
            var teacher = await _teacherRepository
                .Entities
                .SingleOrDefaultAsync(u => u.TeacherNo.Equals(teacherNo));
            if (teacher == null) throw Oops.Oh(TeacherErrorCodes.t1402);
            if (!MD5Encryption.Compare(password, teacher.Password))
            {
                throw Oops.Oh(TeacherErrorCodes.t1403);
            }

            return teacher.Adapt<TeacherDto>();
        }
    }
}