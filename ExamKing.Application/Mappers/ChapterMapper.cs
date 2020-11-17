using ExamKing.Core.Entites;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class ChapterMapper: IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbChapter, ChapterDto>()
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<ChapterDto, TbChapter>()
                .IgnoreNullValues(true);
        }
    }
}