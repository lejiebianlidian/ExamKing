using ExamKing.Core.Entites;
using Furion.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class StuanswerdetailMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbStuanswerdetail, StuanswerdetailDto>()
                .IgnoreNullValues(true); // 忽略空值映射

            config.ForType<StuanswerdetailDto, TbStuanswerdetail>()
                .IgnoreNullValues(true);
        }
    }
}