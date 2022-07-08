using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.Quartz;
using ExamKing.Core.Entites;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Furion.DatabaseAccessor.Extensions;
using Quartz;
using Quartz.Spi;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 考试任务调度服务
    /// </summary>
    public class ExamJobService : IExamJobService, ITransient
    {
        private readonly IRepository<TbExamjobs> _repository;

        private readonly IExamService _examService;
        
        //调度器工厂
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;

        ///  <summary>
        /// 构造函数
        ///  </summary>
        public ExamJobService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IRepository<TbExamjobs> repository
            )
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _repository = repository;
            var examService = App.GetService<IExamService>();
            _examService = examService;

        }

        /// <summary>
        /// 恢复全部未完成考试任务
        /// </summary>
        /// <returns></returns>
        public async Task<List<TbExamjobs>> ReturnNoneExamJobAll()
        {
            var jobs = await _repository.Entities.AsNoTracking()
                .Where(u => u.Status == 0) // 恢复未结束考试
                .Select(u => new TbExamjobs
                {
                    Id = u.Id,
                    ExamId = u.ExamId,
                    Status = u.Status,
                    Exam = new TbExam
                    {
                        Id = u.Exam.Id,
                        ExamName = u.Exam.ExamName,
                        StartTime = u.Exam.StartTime,
                        EndTime = u.Exam.EndTime,
                        Duration = u.Exam.Duration,
                        IsEnable = u.Exam.IsEnable,
                        IsFinish = u.Exam.IsFinish,
                    }
                })
                .ToListAsync();

            foreach (var examJob in jobs)
            {
                await StartExamJob(examJob.ExamId.ToString(), examJob.Exam.StartTime);
            }

            return jobs;
        }

        /// <summary>
        /// 新增考试任务
        /// </summary>
        /// <param name="examjob"></param>
        /// <returns></returns>
        public async Task AddExamJob(TbExamjobs examjob)
        {
            var jobEntity = await _repository.InsertNowAsync(examjob);
            var examJob = await _repository.Entities.AsNoTracking()
                .Where(u => u.Id == jobEntity.Entity.Id)
                .Select(u => new TbExamjobs
                {
                    Id = u.Id,
                    ExamId = u.ExamId,
                    Status = u.Status,
                    Exam = new TbExam
                    {
                        Id = u.Exam.Id,
                        ExamName = u.Exam.ExamName,
                        StartTime = u.Exam.StartTime,
                        EndTime = u.Exam.EndTime,
                        Duration = u.Exam.Duration,
                        IsEnable = u.Exam.IsEnable,
                        IsFinish = u.Exam.IsFinish,
                    }
                })
                .FirstOrDefaultAsync();

            await StartExamJob(examJob.ExamId.ToString(), examjob.Exam.StartTime);
        }

        /// <summary>
        /// 完成考试任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> FinshExamJob(int id)
        {
            var examJob = await _repository.Entities.AsNoTracking()
                .Select(u => new TbExamjobs
                {
                    Id = u.Id,
                    ExamId = u.ExamId,
                    Status = u.Status,
                    Exam = new TbExam
                    {
                        Id = u.Exam.Id,
                        ExamName = u.Exam.ExamName,
                        StartTime = u.Exam.StartTime,
                        EndTime = u.Exam.EndTime,
                        Duration = u.Exam.Duration,
                        IsEnable = u.Exam.IsEnable,
                        IsFinish = u.Exam.IsFinish,
                        Classes = u.Exam.Classes.Select(
                            x => new TbClass
                            {
                                Id = x.Id,
                                Students = x.Students.Select(
                                    s => new TbStudent
                                    {
                                        Id = s.Id,
                                    }).ToList()
                            }).ToList()
                    }
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (examJob == null)
            {
                return false;
            }
            
            // 移除任务调度
            var jobKey = "job_" + examJob.ExamId.ToString();
            JobKey jk = new JobKey(jobKey, "exams");
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.DeleteJob(jk);
            
            // 计算全部考生成绩
            foreach (var classes in examJob.Exam.Classes)
            {
                foreach (var stu in classes.Students)
                {
                    try
                    {
                        // 计算学生考试成绩
                        await _examService.SubmitExamByStudent(examJob.ExamId, stu.Id);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    
                }
            }
            
            // 结束任务调度
            examJob.Status = 1;
            await _repository.UpdateIncludeNowAsync(examJob, new[] { "Status" });
            // 结束开始
            await _examService.FinshExamById(examJob.ExamId);
            return true;
        }

        /// <summary>
        /// 开始考试任务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartExamJob(string examId, DateTimeOffset startTime)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            scheduler.JobFactory = _jobFactory;

            var jobKey = "job_" + examId;
            var triggerKey = "trigger_" + examId;

            IJobDetail job = JobBuilder.Create<ExamJob>()
                .UsingJobData("ExamId", examId)
                .StoreDurably(true)
                .RequestRecovery(true)
                .WithIdentity(jobKey, "exams")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey, "exams")
                .StartAt(startTime)
                .WithSimpleSchedule(
                    // 5s 调度一次任务
                    x => x.WithIntervalInSeconds(5).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            return true;
        }

        /// <summary>
        /// 执行考试任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteExamJob(int id)
        {
            var examJob = await _repository.Entities.AsNoTracking()
                .Select(u => new TbExamjobs
                {
                    Id = u.Id,
                    ExamId = u.ExamId,
                    Status = u.Status,
                    Exam = new TbExam
                    {
                        Id = u.Exam.Id,
                        ExamName = u.Exam.ExamName,
                        StartTime = u.Exam.StartTime,
                        EndTime = u.Exam.EndTime,
                        Duration = u.Exam.Duration,
                        IsEnable = u.Exam.IsEnable,
                        IsFinish = u.Exam.IsFinish,
                    }
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (examJob == null)
            {
                return false;
            }
            // 判断是否可以启用试卷
            var nowTime = DateTimeOffset.Now;
            if (nowTime >= examJob.Exam.StartTime && examJob.Exam.IsEnable == "0")
            {
                await _examService.EnableExamById(examJob.ExamId);
            }
            // 判断是否结束考试
            if (nowTime >= examJob.Exam.EndTime && examJob.Exam.IsFinish == "0")
            {
               await FinshExamJob(examJob.ExamId);
            }
            return true;
        }

        public async Task<bool> UpdateExamJob(int id, DateTimeOffset startTime)
        {
            var examJob = await _repository.Entities.AsNoTracking()
                .Select(u => new TbExamjobs
                {
                    Id = u.Id,
                    ExamId = u.ExamId,
                    Status = u.Status
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (examJob == null)
            {
                return false;
            }
            var jobKey = "job_" + examJob.ExamId.ToString();
            JobKey jk = new JobKey(jobKey, "exams");
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.DeleteJob(jk);

            await StartExamJob(examJob.ExamId.ToString(), startTime);
            return true;
        }
    }
}