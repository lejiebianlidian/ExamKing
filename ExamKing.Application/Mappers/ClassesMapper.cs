using Mapster;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Mappers
{
    /// <inheritdoc />
    public class ClassesMapper : IRegister
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
