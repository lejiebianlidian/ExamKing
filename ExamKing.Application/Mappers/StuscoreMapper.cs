using Mapster;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Mappers
{
    public class StucoreMapper : IRegister
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
