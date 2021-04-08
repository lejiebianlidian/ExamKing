using ExamKing.Core.Entites;

using Mapster;

namespace ExamKing.Application.Mappers
{
    public class CourseMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbCourse, CourseDto>()
                .IgnoreNullValues(true); // 过滤空值

            config.ForType<CourseDto, TbCourse>()
                .IgnoreNullValues(true)
                .IgnoreIf((src, dest) => src.CourseName == "", dest => dest.CourseName)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .IgnoreIf((src, dest) => src.TeacherId <= 0, dest => dest.TeacherId);
        }
    }
}