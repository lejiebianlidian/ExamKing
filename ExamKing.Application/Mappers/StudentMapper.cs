using System;
using Furion.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.DataEncryption;

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
                .Map(desc=>desc.Sex, src=> src.Sex=="0"?"男":src.Sex=="1"?"女":"保密");

            // studentDto.Adapt<TbStudent>();
            config.ForType<StudentDto, TbStudent>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.ClassesId <= 0, dest => dest.ClassesId)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .IgnoreIf((src, dest) => src.StuNo == "", dest => dest.StuNo)
                .IgnoreIf((src, dest) => src.StuName == "", dest => dest.StuName)
                .IgnoreIf((src, dest) => src.IdCard == "", dest => dest.IdCard)
                .IgnoreIf((src, dest) => src.Telphone == "", dest => dest.Telphone)
                .IgnoreIf((src, dest) => src.Sex == "", dest => dest.Sex)
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .Map(desc => desc.Sex,
                    src => src.Sex == "男" ? "0" :
                        src.Sex == "女" ? "1" :
                        src.Sex == "保密" ? "2" : (src.Sex == "保密" || src.Sex == "男" || src.Sex == "女") ? "2" : src.Sex)
                .Map(desc => desc.Password, src => MD5Encryption.Encrypt(src.Password));
        }
    }
}
