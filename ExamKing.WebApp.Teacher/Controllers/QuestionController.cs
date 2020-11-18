using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Mappers;
using ExamKing.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ExamKing.WebApp.Teacher
{
    /// <summary>
    /// 题库接口
    /// </summary>
    public class QuestionController : ApiControllerBase
    {
        private readonly ISelectService _selectService;
        private readonly IJudgeService _judgeService;
        /// <summary>
        /// 依赖注入 
        /// </summary>
        public QuestionController(
            IJudgeService judgeService,
            ISelectService selectService)
        {
            _selectService = selectService;
            _judgeService = judgeService;
        }

        /// <summary>
        /// 创建选择题
        /// </summary>
        /// <param name="addSelectInput"></param>
        /// <returns></returns>
        public async Task<SelectOutput> InsertAddSelect(AddSelectInput addSelectInput)
        {
            var selectInsert = await _selectService.CreateSelect(addSelectInput.Adapt<SelectDto>());
            return selectInsert.Adapt<SelectOutput>();
        }

        /// <summary>
        /// 更新选择题
        /// </summary>
        /// <param name="editSelectInput"></param>
        /// <returns></returns>
        public async Task<SelectOutput> UpdateEditSelect(EditSelectInput editSelectInput)
        {
            var selectUpdate = await _selectService.UpdateSelect(editSelectInput.Adapt<SelectDto>());
            return selectUpdate.Adapt<SelectOutput>();
        }

        /// <summary>
        /// 删除选择题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveSelect(int id)
        {
            await _selectService.DeleteSelect(id);
            return "success";
        }

        /// <summary>
        /// 选择题列表
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<SelectCourseChapterOutput>> GetSelectList(
            [FromQuery] int teacherId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var selectList = await _selectService
                .FindSelectAllByTeacherAndPage(teacherId, pageIndex, pageSize);
            return selectList.Adapt<PagedList<SelectCourseChapterOutput>>();
        }

        /// <summary>
        /// 查询选择题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SelectCourseChapterOutput> GetSelectInfo(int id)
        {
            var selectEntity = await _selectService.FindSelectById(id);
            return selectEntity.Adapt<SelectCourseChapterOutput>();
        }
        
        
        /// <summary>
        /// 创建是非题
        /// </summary>
        /// <param name="addJudgeInput"></param>
        /// <returns></returns>
        public async Task<JudgeOutput> InsertAddJudge(AddJudgeInput addJudgeInput)
        {
            var judgeInsert = await _judgeService.CreateJudge(addJudgeInput.Adapt<JudgeDto>());
            return judgeInsert.Adapt<JudgeOutput>();
        }

        /// <summary>
        /// 更新是非题
        /// </summary>
        /// <param name="editJudgeInput"></param>
        /// <returns></returns>
        public async Task<JudgeOutput> UpdateEditJudge(EditJudgeInput editJudgeInput)
        {
            var judgeUpdate = await _judgeService.UpdateJudge(editJudgeInput.Adapt<JudgeDto>());
            return judgeUpdate.Adapt<JudgeOutput>();
        }

        /// <summary>
        /// 删除是非题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteRemoveJudge(int id)
        {
            await _judgeService.DeleteJudge(id);
            return "success";
        }

        /// <summary>
        /// 是非题列表
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<JudgeCourseChapterOutput>> GetJudgeList(
            [FromQuery] int teacherId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var judgeList = await _judgeService
                .FindJudgeAllByTeacherAndPage(teacherId, pageIndex, pageSize);
            return judgeList.Adapt<PagedList<JudgeCourseChapterOutput>>();
        }

        /// <summary>
        /// 查询是非题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JudgeCourseChapterOutput> GetJudgeInfo(int id)
        {
            var judgeEntity = await _judgeService.FindJudgeById(id);
            return judgeEntity.Adapt<JudgeCourseChapterOutput>();
        }
    }
}