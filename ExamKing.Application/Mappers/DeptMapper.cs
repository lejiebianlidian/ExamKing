using ExamKing.Core.Entites;

using Mapster;

namespace ExamKing.Application.Mappers
{
    public class DeptMapper:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbDept, DeptDto>()
                .IgnoreNullValues(true); // 忽略空值映射;

            config.ForType<DeptDto, TbDept>()
                .IgnoreNullValues(true); // 忽略空值映射;

        }
    }
}