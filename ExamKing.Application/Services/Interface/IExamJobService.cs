using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Services
{
    
    /// <summary>
    /// 考试任务调度服务
    /// </summary>
    public interface IExamJobService
    {
        /// <summary>
        /// 还原未完成到考试任务
        /// </summary>
        /// <returns></returns>
        public Task<List<TbExamjobs>> ReturnNoneExamJobAll();

        /// <summary>
        /// 新增考试任务
        /// </summary>
        /// <param name="examjob"></param>
        /// <returns></returns>
        public Task AddExamJob(TbExamjobs examjob);
        
        /// <summary>
        /// 完成考试任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> FinshExamJob(int id);

        /// <summary>
        /// 开始考试任务
        /// </summary>
        /// <returns></returns>
        public Task<bool> StartExamJob(string examId, DateTimeOffset startTime);
        
        /// <summary>
        /// 执行考试任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ExecuteExamJob(int id);

        /// <summary>
        /// 更新考试任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public Task<bool> UpdateExamJob(int id, DateTimeOffset startTime);
    }
}