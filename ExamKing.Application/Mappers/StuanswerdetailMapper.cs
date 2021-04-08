using ExamKing.Core.Entites;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class StuanswerdetailMapper : IRegister
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