using ExamKing.Application.Services;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class AdminMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbAdmin, AdminDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password)
                .Map(desc => desc.CreateTime,
                    src => TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));

            config.ForType<AdminDto, TbAdmin>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .Map(desc => desc.CreateTime, src => TimeUtil.GetTimeStampNow
                    ());
        }
    }
}