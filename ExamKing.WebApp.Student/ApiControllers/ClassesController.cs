using System.Collections.Generic;
using System.Threading.Tasks;
using ExamKing.Application.Services;
using ExamKing.Application.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 班级接口
    /// </summary>
    public class ClassesController : ApiControllerBase
    {
        private readonly IClassesService _classesService;

        /// <inheritdoc />
        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }

    }
}