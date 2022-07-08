using ExamKing.Core.Entites;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class SelectMapper :IRegister
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