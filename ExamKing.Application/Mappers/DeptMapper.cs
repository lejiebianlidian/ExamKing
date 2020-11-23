using System;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class DeptMapper:IObjectMapper
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