using System;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Furion;
using Microsoft.Extensions.Logging;
using Quartz;

namespace ExamKing.Application.Quartz
{
    /// <summary>
    /// 考试作业
    /// </summary>
    [DisallowConcurrentExecution]
    public class ExamJob : IJob
    {
        /// <summary>
        /// 依赖服务
        /// </summary>
        private readonly ILogger<ExamJob> _logger;
        
        ///  <summary>
        /// 依赖注入
        ///  </summary>
        ///  <param name="logger"></param>
        public ExamJob(
            ILogger<ExamJob> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            int examId = dataMap.GetIntValue("ExamId");
            
            _logger.LogInformation($"ExamId=>{examId} is running");
            
            var examJobService = App.GetService<IExamJobService>();
            // 执行考试任务
            await examJobService.ExecuteExamJob(examId);

        }
    }
}