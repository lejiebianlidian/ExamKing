#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 考试成绩服务
    /// </summary>
    public class StuscoreService : IStuscoreService, ITransient
    {
        private readonly IRepository<TbStuscore> _stuscoreRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="stuscoreRepository"></param>
        public StuscoreService(IRepository<TbStuscore> stuscoreRepository)
        {
            _stuscoreRepository = stuscoreRepository;
        }

        /// <summary>
        /// 查询学生全部考试成绩分页
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<StuscoreDto>> FindScoreAllByStudentAndPage(int studentId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _stuscoreRepository
                .Entities.AsNoTracking()
                .Select(u => new TbStuscore
                {
                    Id = u.Id,
                    StuId = u.StuId,
                    CourseId = u.CourseId,
                    ExamId = u.ExamId,
                    Score = u.Score,
                    CreateTime = u.CreateTime,
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<StuscoreDto>>();
        }

        /// <summary>
        /// 查询学生最新一条成绩
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StuscoreDto?> FindScoreTodayByStudent(int studentId)
        {
            var examScore = await _stuscoreRepository
                .Entities.AsNoTracking()
                .Where(u => u.StuId == studentId)
                .OrderByDescending(u=>u.CreateTime)
                .FirstOrDefaultAsync();
            return examScore?.Adapt<StuscoreDto>();
        }
    }
}