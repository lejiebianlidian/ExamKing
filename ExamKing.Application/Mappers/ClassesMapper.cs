using Furion.ObjectMapper;
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
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<ClassesDto, TbClass>()
                .IgnoreNullValues(true);
        }

    }
}
