using System;
using Fur.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;

namespace ExamKing.Application.Mappers
{
    /// <inheritdoc />
    public class StudentMapper : IObjectMapper
    {
        /// <inheritdoc />
        public void Register(TypeAdapterConfig config)
        {
            // tbStudent.Adapt<StudentDto>();
            config.ForType<TbStudent, StudentDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password)
                .Map(desc=>desc.CreateTime, src=>TimeUtil.GetDateTime(src.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(desc=>desc.Sex, src=> src.Sex=="0"?"男":src.Sex=="1"?"女":"保密");

            // studentDto.Adapt<TbStudent>();
            config.ForType<StudentDto, TbStudent>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.ClassesId <= 0, dest => dest.ClassesId)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .IgnoreIf((src, dest) => src.StuNo == "", dest => dest.StuNo)
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .Map(src => src.CreateTime, desc => TimeUtil.GetTimeStampNow
                    ());
        }
    }
}
