using ExamKing.Core.Entites;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class SelectMapper :IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbSelect, SelectDto>()
                .IgnoreNullValues(true); // 忽略空值映射;

            config.ForType<SelectDto, TbSelect>()
                .IgnoreNullValues(true); // 忽略空值映射;
        }
    }
}