using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Fur.DependencyInjection;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class CourseMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbCourse, CourseDto>()
                .IgnoreNullValues(true) // 过滤空值
                .Map(desc => desc.CreateTime,
                    src => TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));

            config.ForType<CourseDto, TbCourse>()
                .IgnoreNullValues(true)
                .IgnoreIf((src, dest) => src.CourseName == "", dest => dest.CourseName)
                .IgnoreIf((src, dest) => src.Deptld <= 0, dest => dest.Deptld)
                .IgnoreIf((src, dest) => src.Teacherld <= 0, dest => dest.Teacherld)
                .Map(desc => desc.CreateTime, src => TimeUtil.GetTimeStampNow
                    ());
        }
    }
}