using ExamKing.Core.Entites;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class ExamquestionMapper : IRegister
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