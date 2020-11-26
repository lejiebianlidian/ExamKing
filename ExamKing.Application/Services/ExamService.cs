using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamKing.Application.ErrorCodes;
using ExamKing.Application.Mappers;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 试卷服务
    /// </summary>
    public class ExamService : IExamService, ITransient
    {
        private readonly IRepository<TbExam> _examRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        public ExamService(
            IRepository<TbExam> examRepository,
            IRepository<TbClass> classRepository,
            IRepository<TbCourse> courseRepository)
        {
            _examRepository = examRepository;
        }

        /// <summary>
        /// 手动组卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public async Task<ExamDto> CreateExam(ExamDto examDto)
        {
            var exam = await _examRepository
                .InsertNowAsync(examDto.Adapt<TbExam>());
            var examEntity = exam.Entity;
            return examEntity.Adapt<ExamDto>();
        }

        public async Task<ExamDto> AutoCreateExam(ExamDto examDto)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 更新试卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public async Task<ExamDto> UpdateExam(ExamDto examDto)
        {
            var oldExam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == examDto.Id);
            if (oldExam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            // 删除原试卷题目
            await _examRepository
                .Change<TbExamquestion>()
                .DeleteAsync(
                    _examRepository
                        .Change<TbExamquestion>()
                        .Where(u => u.ExamId == oldExam.Id, false).ToList()
                );

            // 更新试卷
            var exam = examDto.Adapt(oldExam);
            await exam
                .UpdateExcludeAsync(u => u.CreateTime);

            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteExam(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            // 删除卷题目
            await _examRepository
                .Change<TbExamquestion>()
                .DeleteAsync(
                    _examRepository
                        .Change<TbExamquestion>()
                        .Where(u => u.ExamId == id, false).ToList()
                );
            // 删除试卷班级关联
            await _examRepository
                .Change<TbExamclass>()
                .DeleteAsync(
                    _examRepository
                        .Change<TbExamclass>()
                        .Where(u => u.ExamId == id, false).ToList()
                );
            await _examRepository.DeleteAsync(exam);
        }

        /// <summary>
        /// 根据教师查询分页试卷
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamAllByTeacherAndPage(int teacherId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _examRepository
                .Entities.AsNoTracking()
                .Where(u => u.TeacherId == teacherId)
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    CourseId = u.CourseId,
                    TeacherId = u.TeacherId,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    Duration = u.Duration,
                    IsEnable = u.IsEnable,
                    IsFinish = u.IsFinish,
                    CreateTime = u.CreateTime,
                    ExamScore = u.ExamScore,
                    JudgeScore = u.JudgeScore,
                    SingleScore = u.SingleScore,
                    SelectScore = u.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Teacher.Id,
                        TeacherName = u.Teacher.TeacherName,
                    }
                })
                .ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 查询试卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> FindExamById(int id)
        {
            var exam = await _examRepository
                .Entities
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    CourseId = u.CourseId,
                    TeacherId = u.TeacherId,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    Duration = u.Duration,
                    IsEnable = u.IsEnable,
                    IsFinish = u.IsFinish,
                    CreateTime = u.CreateTime,
                    ExamScore = u.ExamScore,
                    JudgeScore = u.JudgeScore,
                    SingleScore = u.SingleScore,
                    SelectScore = u.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Teacher.Id,
                        TeacherName = u.Teacher.TeacherName,
                    },
                    Examquestions = u.Examquestions.Select(x => new TbExamquestion
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType,
                        QuestionId = x.QuestionId,
                        Score = x.Score,
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 启用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> EnableExamById(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            exam.IsEnable = "1";
            await exam.UpdateExcludeAsync(u => u.CreateTime);
            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 关闭试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> DisableExamById(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            exam.IsEnable = "0";
            await exam.UpdateExcludeAsync(u => u.CreateTime);
            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> StartExamById(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            exam.IsFinish = "0";
            await exam.UpdateExcludeAsync(u => u.CreateTime);
            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 结束试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> FinshExamById(int id)
        {
            var exam = await _examRepository
                .FirstOrDefaultAsync(u => u.Id == id);
            if (exam == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            exam.IsFinish = "1";
            await exam.UpdateExcludeAsync(u => u.CreateTime);
            return exam.Adapt<ExamDto>();
        }

        /// <summary>
        /// 根据班级查询最新考试列表
        /// </summary>
        /// <param name="classesId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamNewByClassesAndPage(
            int classesId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _examRepository.Change<TbExamclass>()
                .Entities.AsNoTracking()
                .Where(u => u.Classes.Id == classesId)
                .Where(u => u.Exam.IsFinish == "0")
                .OrderBy(u => u.Exam.IsFinish)
                .ThenByDescending(u => u.Exam.StartTime)
                .Select(u => new TbExam
                {
                    Id = u.Exam.Id,
                    ExamName = u.Exam.ExamName,
                    CourseId = u.Exam.CourseId,
                    TeacherId = u.Exam.TeacherId,
                    StartTime = u.Exam.StartTime,
                    EndTime = u.Exam.EndTime,
                    Duration = u.Exam.Duration,
                    IsEnable = u.Exam.IsEnable,
                    IsFinish = u.Exam.IsFinish,
                    CreateTime = u.Exam.CreateTime,
                    ExamScore = u.Exam.ExamScore,
                    JudgeScore = u.Exam.JudgeScore,
                    SingleScore = u.Exam.SingleScore,
                    SelectScore = u.Exam.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Exam.Course.Id,
                        CourseName = u.Exam.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Exam.Teacher.Id,
                        TeacherName = u.Exam.Teacher.TeacherName,
                    },
                }).ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据班级查询正在考试列表
        /// </summary>
        /// <param name="classesId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamOnlineByClassesAndPage(
            int classesId, int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _examRepository.Change<TbExamclass>()
                .Entities.AsNoTracking()
                .Where(u => u.Classes.Id == classesId)
                .Where(u => u.Exam.IsEnable == "1" && u.Exam.IsFinish == "0")
                .OrderByDescending(u => u.Exam.StartTime)
                .Select(u => new TbExam
                {
                    Id = u.Exam.Id,
                    ExamName = u.Exam.ExamName,
                    CourseId = u.Exam.CourseId,
                    TeacherId = u.Exam.TeacherId,
                    StartTime = u.Exam.StartTime,
                    EndTime = u.Exam.EndTime,
                    Duration = u.Exam.Duration,
                    IsEnable = u.Exam.IsEnable,
                    IsFinish = u.Exam.IsFinish,
                    CreateTime = u.Exam.CreateTime,
                    ExamScore = u.Exam.ExamScore,
                    JudgeScore = u.Exam.JudgeScore,
                    SingleScore = u.Exam.SingleScore,
                    SelectScore = u.Exam.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Exam.Course.Id,
                        CourseName = u.Exam.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Exam.Teacher.Id,
                        TeacherName = u.Exam.Teacher.TeacherName,
                    },
                    Examquestions = u.Exam.Examquestions.Select(x => new TbExamquestion
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType,
                        QuestionId = x.QuestionId,
                        Score = x.Score,
                    }).ToList()
                }).ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据班级查询未考试列表
        /// </summary>
        /// <param name="classesId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamWaitByClassesAndPage(int classesId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _examRepository.Change<TbExamclass>()
                .Entities.AsNoTracking()
                .Where(u => u.Classes.Id == classesId)
                .Where(u => u.Exam.IsEnable == "0" && u.Exam.IsFinish == "0")
                .OrderByDescending(u => u.Exam.StartTime)
                .Select(u => new TbExam
                {
                    Id = u.Exam.Id,
                    ExamName = u.Exam.ExamName,
                    CourseId = u.Exam.CourseId,
                    TeacherId = u.Exam.TeacherId,
                    StartTime = u.Exam.StartTime,
                    EndTime = u.Exam.EndTime,
                    Duration = u.Exam.Duration,
                    IsEnable = u.Exam.IsEnable,
                    IsFinish = u.Exam.IsFinish,
                    CreateTime = u.Exam.CreateTime,
                    ExamScore = u.Exam.ExamScore,
                    JudgeScore = u.Exam.JudgeScore,
                    SingleScore = u.Exam.SingleScore,
                    SelectScore = u.Exam.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Exam.Course.Id,
                        CourseName = u.Exam.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Exam.Teacher.Id,
                        TeacherName = u.Exam.Teacher.TeacherName,
                    },
                    Examquestions = u.Exam.Examquestions.Select(x => new TbExamquestion
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType,
                        QuestionId = x.QuestionId,
                        Score = x.Score,
                    }).ToList()
                }).ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据班级查询已考试列表
        /// </summary>
        /// <param name="classesId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamFinshByClassesAndPage(int classesId, int pageIndex = 1,
            int pageSize = 10)
        {
            var pageResult = await _examRepository.Change<TbExamclass>()
                .Entities.AsNoTracking()
                .Where(u => u.Classes.Id == classesId)
                .Where(u => u.Exam.IsEnable == "1" && u.Exam.IsFinish == "1")
                .OrderByDescending(u => u.Exam.StartTime)
                .Select(u => new TbExam
                {
                    Id = u.Exam.Id,
                    ExamName = u.Exam.ExamName,
                    CourseId = u.Exam.CourseId,
                    TeacherId = u.Exam.TeacherId,
                    StartTime = u.Exam.StartTime,
                    EndTime = u.Exam.EndTime,
                    Duration = u.Exam.Duration,
                    IsEnable = u.Exam.IsEnable,
                    IsFinish = u.Exam.IsFinish,
                    CreateTime = u.Exam.CreateTime,
                    ExamScore = u.Exam.ExamScore,
                    JudgeScore = u.Exam.JudgeScore,
                    SingleScore = u.Exam.SingleScore,
                    SelectScore = u.Exam.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Exam.Course.Id,
                        CourseName = u.Exam.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Exam.Teacher.Id,
                        TeacherName = u.Exam.Teacher.TeacherName,
                    },
                    Examquestions = u.Exam.Examquestions.Select(x => new TbExamquestion
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType,
                        QuestionId = x.QuestionId,
                        Score = x.Score,
                    }).ToList()
                }).ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据关键词搜索考试分页
        /// </summary>
        /// <param name="classesId">学生ID</param>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<ExamDto>> FindExamByKeywordAndStudentAndPage(int classesId, string keyword,
            int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _examRepository.Change<TbExamclass>()
                .Entities.AsNoTracking()
                .Where(u => u.Classes.Id == classesId && u.Exam.ExamName.Contains(keyword))
                .OrderByDescending(u => u.Exam.StartTime)
                .Select(u => new TbExam
                {
                    Id = u.Exam.Id,
                    ExamName = u.Exam.ExamName,
                    CourseId = u.Exam.CourseId,
                    TeacherId = u.Exam.TeacherId,
                    StartTime = u.Exam.StartTime,
                    EndTime = u.Exam.EndTime,
                    Duration = u.Exam.Duration,
                    IsEnable = u.Exam.IsEnable,
                    IsFinish = u.Exam.IsFinish,
                    CreateTime = u.Exam.CreateTime,
                    ExamScore = u.Exam.ExamScore,
                    JudgeScore = u.Exam.JudgeScore,
                    SingleScore = u.Exam.SingleScore,
                    SelectScore = u.Exam.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Exam.Course.Id,
                        CourseName = u.Exam.Course.CourseName,
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Exam.Teacher.Id,
                        TeacherName = u.Exam.Teacher.TeacherName,
                    },
                    Examquestions = u.Exam.Examquestions
                        .Select(x => new TbExamquestion
                    {
                        Id = x.Id,
                        QuestionType = x.QuestionType,
                        QuestionId = x.QuestionId,
                        Score = x.Score,
                    }).ToList()
                }).ToPagedListAsync(pageIndex, pageSize);

            return pageResult.Adapt<PagedList<ExamDto>>();
        }

        /// <summary>
        /// 根据学生查询考试结果信息
        /// </summary>
        /// <param name="id">考试ID</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExamDto> FindExamResultByStudent(int id, int studentId)
        {
            var result = await _examRepository
                .Entities.AsNoTracking()
                .Where(u => u.Id == id)
                .Include(u => u.Examquestions)
                .Include(u => u.Stuanswerdetails)
                .Include(u => u.Stuscores)
                .Select(u => new TbExam
                {
                    Id = u.Id,
                    ExamName = u.ExamName,
                    CourseId = u.CourseId,
                    TeacherId = u.TeacherId,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    Duration = u.Duration,
                    IsEnable = u.IsEnable,
                    IsFinish = u.IsFinish,
                    CreateTime = u.CreateTime,
                    ExamScore = u.ExamScore,
                    JudgeScore = u.JudgeScore,
                    SingleScore = u.SingleScore,
                    SelectScore = u.SelectScore,
                    Course = new TbCourse
                    {
                        Id = u.Course.Id,
                        CourseName = u.Course.CourseName
                    },
                    Teacher = new TbTeacher
                    {
                        Id = u.Teacher.Id,
                        TeacherName = u.Teacher.TeacherName
                    },
                    Examquestions = u.Examquestions
                        .Where(x => x.ExamId == u.Id).OrderBy(x => x.Id)
                        .Select(x => new TbExamquestion
                        {
                            Id = x.Id,
                            QuestionType = x.QuestionType,
                        }).ToList(),
                    Stuanswerdetails = u.Stuanswerdetails
                        .Where(x => x.StuId == studentId && x.ExamId == id)
                        .Select(x => new TbStuanswerdetail
                        {
                            Id = x.Id,
                            QuestionId = x.QuestionId,
                            QuestionType = x.QuestionType,
                            Isright = x.Isright,
                        }).ToList(),
                    Stuscores = u.Stuscores
                        .Where(x => x.StuId == studentId && x.ExamId == id)
                        .Select(x => new TbStuscore
                        {
                            Id = x.Id,
                            Score = x.Score,
                        }).ToList(),
                }).FirstOrDefaultAsync();

            if (result == null)
            {
                throw Oops.Oh(ExamErrorCodes.s1901);
            }

            return result.Adapt<ExamDto>();
        }

        
        /// <summary>
        /// 根据学生交卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StuscoreDto> SubmitExamByStudent(int id, int studentId)
        {
            var exam = await this.FindExamById(id);
            // 判断是否已经交卷
            var hasScore = await _examRepository.Change<TbStuscore>()
                .Entities.AsNoTracking()
                .Where(u => u.ExamId == id && u.StuId == studentId)
                .FirstOrDefaultAsync();
            if (hasScore!=null)
            {
                throw Oops.Oh(ExamScoreErrorCodes.k2002);
            }
            //  计算成绩
            var score = await _examRepository.Change<TbStuanswerdetail>()
                .Entities
                .Where(u=>u.ExamId==exam.Id && u.StuId==studentId && u.Isright == "1")
                .SumAsync(u=>u.Examquestion.Score);
            
            // 记录分数
            var stuScore = new StuscoreDto
            {
                StuId = studentId,
                CourseId = exam.CourseId,
                ExamId = exam.Id,
                Score = score
            };

            var stuScoreInsert = await _examRepository.Change<TbStuscore>()
                .InsertNowAsync(stuScore.Adapt<TbStuscore>());

            return stuScoreInsert.Entity.Adapt<StuscoreDto>();
        }
    }
}