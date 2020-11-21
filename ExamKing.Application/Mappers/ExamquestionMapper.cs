using ExamKing.Core.Entites;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class ExamquestionMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbExamquestion, ExamquestionDto>()
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<ExamquestionDto, TbExamquestion>()
                .IgnoreNullValues(true);
        }
    }
}