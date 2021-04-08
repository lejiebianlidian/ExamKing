using ExamKing.Core.Entites;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class JudgeMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbJudge, JudgeDto>()
                .IgnoreNullValues(true); // 忽略空值映射;

            config.ForType<JudgeDto, TbJudge>()
                .IgnoreNullValues(true); // 忽略空值映射;
        }
    }
}