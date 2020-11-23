using System;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Core.Entites;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 答题服务
    /// </summary>
    public class StuanswerdetailService : IStuanswerdetailService, ITransient
    {
        
        private readonly IRepository<TbStuanswerdetail> _answerRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public StuanswerdetailService(IRepository<TbStuanswerdetail> answerRepository)
        {
            _answerRepository = answerRepository;
        }
        
        /// <summary>
        /// 获取学生全部错题数量
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerByStudent(int studentId)
        {
            var count = await _answerRepository
                .Where(x => x.StuId == studentId)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// 获取学生今日错题数
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerTodayByStudent(int studentId)
        {
            var today = DateTimeOffset.UtcNow;
            var count = await _answerRepository
                .Where(x => x.StuId == studentId)
                .Where(x=>x.CreateTime.Date==today.Date)
                .CountAsync();
            return count;
        }
    }
}