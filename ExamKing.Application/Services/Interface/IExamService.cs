using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 试卷服务接口
    /// </summary>
    public interface IExamService
    {
        /// <summary>
        /// 手动组卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public Task<ExamDto> CreateExam(ExamDto examDto);
        
        /// <summary>
        /// 自动组卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public Task<ExamDto> AutoCreateExam(ExamDto examDto);

        /// <summary>
        /// 更新试卷
        /// </summary>
        /// <param name="examDto"></param>
        /// <returns></returns>
        public Task<ExamDto> UpdateExam(ExamDto examDto);

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteExam(int id);

        /// <summary>
        /// 根据教师查询分页试卷
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamAllByTeacherAndPage(int teacherId, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据Id查询试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ExamDto> FindExamById(int id);

        /// <summary>
        /// 启用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ExamDto> EnableExamById(int id);
        
        /// <summary>
        /// 关闭试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ExamDto> DisableExamById(int id);
        
        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ExamDto> StartExamById(int id);
        
        /// <summary>
        /// 结束考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ExamDto> FinshExamById(int id);
        
        /// <summary>
        /// 根据班级查询正在考试列表
        /// </summary>
        /// <param name="classesId">班级ID</param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamNewByClassesAndPage(int classesId, int pageIndex = 1, int pageSize = 10);

        
        /// <summary>
        /// 根据班级查询正在考试列表
        /// </summary>
        /// <param name="classesId">班级ID</param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamOnlineByClassesAndPage(int classesId, int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 根据班级查询未考试列表
        /// </summary>
        /// <param name="classesId">班级ID</param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamWaitByClassesAndPage(int classesId, int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 根据班级查询已考试列表
        /// </summary>
        /// <param name="classesId">班级ID</param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamFinshByClassesAndPage(int classesId, int pageIndex = 1, int pageSize = 10);
        
        /// <summary>
        /// 学生关键词搜索考试分页接口
        /// </summary>
        /// <param name="classesId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<PagedList<ExamDto>> FindExamByKeywordAndStudentAndPage(int classesId, string keyword, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 根据学生查询考试结果信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<ExamDto> FindExamResultByStudent(int id, int studentId);

    }
}