using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.DataEncryption;
using Furion.ObjectMapper;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class TeacherMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbTeacher, TeacherDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password)
                .Map(desc => desc.Sex, src => src.Sex == "0" ? "男" : src.Sex == "1" ? "女" : "保密");

            config.ForType<TeacherDto, TbTeacher>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .IgnoreIf((src, dest) => src.TeacherName == "", dest => dest.TeacherName)
                .IgnoreIf((src, dest) => src.Sex == "", dest => dest.Sex)
                .IgnoreIf((src, dest) => src.Telphone == "", dest => dest.Telphone)
                .IgnoreIf((src, dest) => src.IdCard == "", dest => dest.IdCard)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .Map(desc => desc.Sex,
                    src => src.Sex == "男" ? "0" :
                        src.Sex == "女" ? "1" :
                        src.Sex == "保密" ? "2" : (src.Sex == "保密" || src.Sex == "男" || src.Sex == "女") ? "2" : src.Sex)
                .Map(desc => desc.Password, src => MD5Encryption.Encrypt(src.Password));
        }
    }
}