using ExamKing.Core.Entites;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class ExamMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbExam, ExamDto>()
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<ExamDto, TbExam>()
                .IgnoreNullValues(true);
        }
    }
}