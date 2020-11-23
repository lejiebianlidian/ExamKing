using Fur.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;

namespace ExamKing.Application.Mappers
{
    public class StucoreMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbStuscore, StuscoreDto>()
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<StuscoreDto, TbStuscore>()
                .IgnoreNullValues(true);
        }

    }
}
