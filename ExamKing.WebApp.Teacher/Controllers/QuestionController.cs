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
            var selectDto = addSelectInput.Adapt<SelectDto>();
            selectDto.TeacherId = GetUserId();
            var selectInsert = await _selectService.CreateSelect(selectDto);
            return selectInsert.Adapt<SelectOutput>();
        }

        /// <summary>
        /// 更新选择题
        /// </summary>
        /// <param name="editSelectInput"></param>
        /// <returns></returns>
        public async Task<SelectOutput> UpdateEditSelect(EditSelectInput editSelectInput)
        {
            var edit = editSelectInput.Adapt<SelectDto>();
            edit.TeacherId = GetUserId();
            var selectUpdate = await _selectService.UpdateSelect(edit);
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
        /// <param name="isSingle">是否单选</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<SelectCourseChapterOutput>> GetSelectList(
            [FromQuery] bool isSingle = false,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {

            var teacherId = GetUserId();
            var selectList = await _selectService
                .FindSelectAllByTeacherAndPage(teacherId, isSingle,pageIndex, pageSize);
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
            var addJudgeDto = addJudgeInput.Adapt<JudgeDto>();
            addJudgeDto.TeacherId = GetUserId();
            var judgeInsert = await _judgeService.CreateJudge(addJudgeDto);
            return judgeInsert.Adapt<JudgeOutput>();
        }

        /// <summary>
        /// 更新是非题
        /// </summary>
        /// <param name="editJudgeInput"></param>
        /// <returns></returns>
        public async Task<JudgeOutput> UpdateEditJudge(EditJudgeInput editJudgeInput)
        {
            var edit = editJudgeInput.Adapt<JudgeDto>();
            edit.TeacherId = GetUserId();
            var judgeUpdate = await _judgeService.UpdateJudge(edit);
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
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<JudgeCourseChapterOutput>> GetJudgeList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var teacherId = GetUserId();
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