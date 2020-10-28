using System.Threading.Tasks;
using ExamKing.Core.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using Fur.DatabaseAccessor;
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
        public StudentService(IRepository<TbStudent> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// 学生登录
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<StudentDto> Login(string studentNo, string password)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.StuNo.Equals(studentNo) && s.Password.Equals(password));
            if (student == null) throw Oops.Oh(StudentErrorCodes.s1000);
            return student.Adapt<StudentDto>();
        }


        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        public async Task<StudentDto> Register(StudentDto studentDto)
        {
            // 判断班级是否存在
            var classes = await _studentRepository.Change<TbClass>().FirstOrDefaultAsync(x => x.Id == studentDto.ClassesId);
            if (classes == null) throw Oops.Oh(StudentErrorCodes.s1001);
            // 判断班级是否属于该系别
            if (classes.Deptld != studentDto.DeptId) throw Oops.Oh(StudentErrorCodes.s1002);
            var stduent = await _studentRepository.InsertNowAsync(studentDto.Adapt<TbStudent>());
            return stduent.Entity.Adapt<StudentDto>();
        }

        public async Task<StudentDto> GetInfoById(int Id)
        {
            var student = await _studentRepository
                .Include(x =>x.Classes)
                .Include(x=>x.Dept)
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (student == null) throw Oops.Oh(StudentErrorCodes.s1003);
            return student.Adapt<StudentDto>();
        }
    }
}