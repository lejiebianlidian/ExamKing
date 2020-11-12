using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class TeacherMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbTeacher, TeacherDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password)
                .Map(desc=>desc.Sex, src=> src.Sex=="0"?"男":src.Sex=="1"?"女":"保密")
                .Map(desc => desc.CreateTime,
                    src => TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));

            config.ForType<TeacherDto, TbTeacher>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .IgnoreIf((src, dest) => src.TeacherName == "", dest => dest.TeacherName)
                .IgnoreIf((src, dest) => src.Sex == "", dest => dest.Sex)
                .IgnoreIf((src, dest) => src.Telphone == "", dest => dest.Telphone)
                .IgnoreIf((src, dest) => src.IdCard == "", dest => dest.IdCard)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .Map(desc=>desc.Sex, src=> src.Sex)
                .Map(desc => desc.CreateTime, src => TimeUtil.GetTimeStampNow
                    ());
        }
    }
}