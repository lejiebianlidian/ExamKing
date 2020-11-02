using System;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Fur.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class DeptMapper:IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbDept, DeptDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Map(desc => desc.CreateTime, src => TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));

            config.ForType<DeptDto, TbDept>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Map(src => src.CreateTime, desc => TimeUtil.GetTimeStampNow());
        }
        
        
    }
}