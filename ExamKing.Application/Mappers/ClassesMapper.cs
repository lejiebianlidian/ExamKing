using Fur.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;

namespace ExamKing.Application.Mappers
{
    /// <inheritdoc />
    public class ClassesMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbClass, ClassesDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Map(desc => desc.CreateTime,
                    src => TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));
            config.ForType<ClassesDto, TbClass>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Map(desc => desc.CreateTime, src => TimeUtil.GetTimeStampNow());
        }
        
        
    }
}
