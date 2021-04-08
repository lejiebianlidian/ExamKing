using System.Threading;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using Furion;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace ExamKing.Student.Quartz
{
    /// <summary>
    /// 考试调度服务
    /// </summary>
    public class ExamQuartzHostedService : IHostedService
    {
        //调度器工厂
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        
        /// <summary>
        /// Scheduler
        /// </summary>
        public IScheduler Scheduler { get; set; }
        
        /// <summary>
        /// 依赖注入
        /// </summary>
        public ExamQuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory)
        {
            //注入调度器工厂
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }
        
        /// <summary>
        /// Start
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartExamJobs();
        }

        /// <summary>
        /// Stop
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Scheduler != null) await Scheduler?.Shutdown();
        }

        /// <summary>
        /// 启动全部考试任务
        /// </summary>
        private async Task StartExamJobs()
        {
            // 从工厂获取调度器实例
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            scheduler.JobFactory = _jobFactory;
            // 开始调度
            await scheduler.Start();
            
            // 恢复全部考试任务调度
            if (App.ServiceProvider.GetService(typeof(IExamJobService)) is IExamJobService examJobService)
            {
                await examJobService.ReturnNoneExamJobAll();
            }
        }
    }
}